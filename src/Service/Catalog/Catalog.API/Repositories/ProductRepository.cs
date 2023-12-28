using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;

        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;

        }
        public async Task CreateProduct(Product product)
        {
            await _catalogContext.Products.InsertOneAsync(product);
        }
        public async Task<bool> UpdateProduct(Product product)
        {
            // ???
            var updateResult = await _catalogContext.Products.ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            var deleteResult = await _catalogContext.Products.DeleteOneAsync(p => p.Id == id);
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _catalogContext.Products.Find(p => true).ToListAsync();
        }

        public async Task<Product> GetProduct(string id)
        {
            return await _catalogContext.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
        {
            return await _catalogContext.Products.Find(p => p.Category == categoryName).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByName(string name)
        {
            return await _catalogContext.Products.Find(p => p.Name == name).ToListAsync();
        }

    }
}