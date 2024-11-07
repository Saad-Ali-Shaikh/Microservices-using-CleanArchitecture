using Catalog.Core.Entities;
using Catalog.Core.Reporsitories;
using Catalog.Core.Specs;
using Catalog.Infrastructure.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, IBrandRepository, ITypesRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
            return product;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deletedProduct = await _catalogContext.Products.DeleteOneAsync(x => x.Id == id);
            return deletedProduct.IsAcknowledged && deletedProduct.DeletedCount > 0;
        }

        public async Task<IEnumerable<ProductBrand>> GetAllBrands()
        {
            return await _catalogContext.Brands.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllTypes()
        {

            return await _catalogContext.Types.Find(x => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByBrand(string brandName)
        {
            return await _catalogContext.Products.Find(x => x.Brands.Name.ToLower() == brandName.ToLower()).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            return await _catalogContext.Products.Find(x => x.Name.ToLower() == name.ToLower()).ToListAsync();
        }

        public async Task<Pagination<Product>> GetProducts(CatalogSpecParams catalogSpecParams)
        {
            var builder = Builders<Product>.Filter;
            var filter = builder.Empty;
            if (!string.IsNullOrEmpty(catalogSpecParams.Search))
            {
                filter = filter & builder.Where(p => p.Name.ToLower().Contains(catalogSpecParams.Search.ToLower()));
            }
            if (!string.IsNullOrEmpty(catalogSpecParams.BrandId))
            {
                var brandFilter = builder.Eq(x => x.Brands.Id, catalogSpecParams.BrandId);
                filter &= brandFilter;
            }

            if (!string.IsNullOrEmpty(catalogSpecParams.TypeId))
            {
                var typeFilter = builder.Eq(x => x.Types.Id, catalogSpecParams.TypeId);
                filter &= typeFilter;
            }
            var totalItems = await _catalogContext.Products.CountDocumentsAsync(filter);
            var data = await DataFilter(catalogSpecParams, filter);
            //var data = await _catalogContext.Products.Find(filter).Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize).Limit(catalogSpecParams.PageSize).ToListAsync();

            return new Pagination<Product>(catalogSpecParams.PageIndex, catalogSpecParams.PageSize, (int)totalItems, data);
        }

        private async Task<IReadOnlyList<Product>> DataFilter(CatalogSpecParams catalogSpecParams, FilterDefinition<Product> filter)
        {
            var sortDefinition = Builders<Product>.Sort.Ascending("Name");
            if (!string.IsNullOrEmpty(catalogSpecParams.Sort))
            {
                switch (catalogSpecParams.Sort)
                {
                    case "priceAsc":
                        sortDefinition = Builders<Product>.Sort.Ascending(p => p.Price);
                        break;
                    case "priceDesc":
                        sortDefinition = Builders<Product>.Sort.Descending(p => p.Price);
                        break;
                    default:
                        sortDefinition = Builders<Product>.Sort.Ascending(p => p.Name);
                        break;
                }
            }
            
            return await _catalogContext.Products.Find(filter).Sort(sortDefinition)
                //.Skip((catalogSpecParams.PageIndex - 1) * catalogSpecParams.PageSize)
                .Skip((catalogSpecParams.PageIndex * catalogSpecParams.PageSize) - 1)
                .Limit(catalogSpecParams.PageSize).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var updatedProduct = await _catalogContext.Products.ReplaceOneAsync(x => x.Id == product.Id, product);
            return updatedProduct.IsAcknowledged && updatedProduct.ModifiedCount > 0;
        }
    }
}
