using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions
{
    public static class DbExtensions
    {
        public static IHost MigrateDatabase<TContext>(this IHost host)
        {

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var config = services.GetRequiredService<IConfiguration>();
                var logger = services.GetRequiredService<ILogger<TContext>>();
                try
                {
                    logger.LogInformation("Discount DB Migration Started");
                    ApplyMigration(config);
                    logger.LogInformation("Discount DB Migration Completed");

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }
            }
            return host;
        }

        private static void ApplyMigration(IConfiguration config)
        {
            var retryCount = 5;
            while (retryCount > 0)
            {
                try
                {
                    using (var connection = new NpgsqlConnection(config.GetValue<string>("DatabaseSettings:ConnectionString")))
                    {
                        connection.Open();

                        NpgsqlCommand command = new NpgsqlCommand() { Connection = connection };

                        string query = "DROP TABLE IF EXISTS Coupon";
                        command.CommandText = query;
                        command.ExecuteNonQuery();

                        query = @"CREATE TABLE Coupon(Id Serial PRIMARY KEY,ProductName varchar(500) not null,Description TEXT, Amount INT)";
                        command.CommandText = query;
                        command.ExecuteNonQuery();


                        query = @"INSERT INTO Coupon(ProductName,Description,Amount) VALUES ('Nike', 'Nike Shoes', 500)";
                        command.CommandText = query;
                        command.ExecuteNonQuery();

                        query = @"INSERT INTO Coupon(ProductName,Description,Amount) VALUES ('Adidas', 'Adidas Shoes', 400)";
                        command.CommandText = query;
                        command.ExecuteNonQuery();

                        connection.Close();
                    }
                    break;
                }
                catch (Exception ex)
                {
                    retryCount--;
                    if (retryCount == 0)
                    {
                        throw;
                    }
                    Thread.Sleep(2000);
                }
            }

        }
    }
}
