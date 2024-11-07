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
    public static class TypeContextSeed
    {
        public static void SeedData(IMongoCollection<ProductType> productTypeCollection)
        {
            bool checkTypes = productTypeCollection.Find(x => true).Any();
            string path = Path.Combine("Data", "SeedData", "types.json");
            if (!checkTypes)
            {
                var typesData = File.ReadAllText(path);
                //var typesData = File.ReadAllText("C:\\Saad\\Dev\\SourceCode\\EShopping\\EShopping\\Services\\Catalog\\Catalog.Infrastructure\\Data\\SeedData\\types.json");

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if (types != null)
                {
                    foreach (var type in types)
                    {
                        productTypeCollection.InsertOneAsync(type);
                    }
                }
            }

        }
    }
}
