using Dapper;
using Discount.Core.Entities;
using Discount.Core.Repositories;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Discount.Infrastructure.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration configuration;
        public static string ConnectionString { get; set; }
        public DiscountRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            ConnectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            string query = "INSERT INTO Coupon(ProductName, Description,Amount) values (@ProductName,@Description,@Amount)";
            int affectedRows = -1;
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                affectedRows = await connection.ExecuteAsync(query, new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
                connection.Close();

            }
            return affectedRows == 0 ? false : true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            string query = "DELETE FROM Coupon WHERE ProductName = @ProductName";
            int affectedRows = -1;
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                affectedRows = await connection.ExecuteAsync(query, new { ProductName = productName });
                connection.Close();

            }
            return affectedRows == 0 ? false : true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            Coupon coupon = null;
            string query = "SELECT * FROM Coupon where ProductName = @ProductName";
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(query, new { ProductName = productName });
                if (coupon == null)
                {
                    return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No discounts available" };
                }
                connection.Close();

            }
            return coupon;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            string query = "Update Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount";
            int affectedRows = -1;
            await using (var connection = new NpgsqlConnection(ConnectionString))
            {
                connection.Open();

                affectedRows = await connection.ExecuteAsync(query, new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });
                connection.Close();

            }
            return affectedRows == 0 ? false : true;
        }
    }
}
