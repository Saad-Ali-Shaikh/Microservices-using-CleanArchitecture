using Catalog.Core.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Data
{
    public static class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool checkProducts = productCollection.Find(x => true).Any();
            string path = Path.Combine("Data", "SeedData", "products.json");
            if (!checkProducts)
            {
                //C:\Saad\Dev\SourceCode\EShopping\EShopping\Services\Catalog\Catalog.Infrastructure\Data\SeedData
                //var productsData = File.ReadAllText("C:\\Saad\\Dev\\SourceCode\\EShopping\\EShopping\\Services\\Catalog\\Catalog.Infrastructure\\Data\\SeedData\\products.json");
                var productsData = File.ReadAllText(path);
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products != null)
                {
                    foreach (var product in products)
                    {
                        productCollection.InsertOneAsync(product);
                    }
                }
            }
        }
    }
}
