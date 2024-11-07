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
    public static class BrandContextSeed
    {
        public static void SeedData(IMongoCollection<ProductBrand> brandCollection)
        {
            bool checkBrands = brandCollection.Find(x => true).Any();
            string path = Path.Combine("Data", "SeedData", "brands.json");
            if (!checkBrands)
            {
                var brandData = File.ReadAllText(path);
                //var brandData = File.ReadAllText("C:\\Saad\\Dev\\SourceCode\\EShopping\\EShopping\\Services\\Catalog\\Catalog.Infrastructure\\Data\\SeedData\\brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                if (brands != null)
                {
                    foreach (var brand in brands) {
                        brandCollection.InsertOneAsync(brand);
                    }
                }
            }

        }
    }
}
