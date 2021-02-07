using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Persistence.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Core.Application.Implementations.Product
{
    public class ProductService : IProductService
    {
        protected readonly IStoreDbContext DbContext;

        public ProductService(IStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<Models.Products.Product>> GetAll(CancellationToken cancellationToken = default)
        {
            var products = await DbContext.Products
                .Select(product => Map(product))
                .ToListAsync(cancellationToken);

            return products;
        }

        public async Task<Models.Products.Product> GetId(int id, CancellationToken cancellationToken = default)
        {
            var product = await DbContext.Products
                .Where(x=>x.ProductId == id)
                .Select(productDb => Map(productDb))
                .FirstOrDefaultAsync(cancellationToken);

            return product;
        }

        public async Task<Models.Products.Product> Create(Models.Products.Product model, CancellationToken cancellationToken = default)
        {
            var product = await DbContext.Products.AddAsync(Map(model), cancellationToken);

            await DbContext.SaveChangesAsync(cancellationToken);
            return Map(product.Entity);
        }

        public async Task<Models.Products.Product> Edit(Models.Products.Product model, CancellationToken cancellationToken = default)
        {
            var productDb = await DbContext.Products
                .FirstOrDefaultAsync(x=>x.ProductId == model.ProductId, cancellationToken);

            productDb = DbContext.Products.Update(Map(productDb, model)).Entity;

            await DbContext.SaveChangesAsync(cancellationToken);
            
            return Map(productDb);
        }

        public async Task Delete(Models.Products.Product model, CancellationToken cancellationToken = default)
        {
            var productDb = await DbContext.Products
                .Where(x=> x.ProductId == model.ProductId)
                .FirstOrDefaultAsync(cancellationToken);

            DbContext.Products.Remove(productDb);

            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Products
                .AnyAsync(x => x.ProductId == id, cancellationToken);
        }
        
        
        private static Models.Products.Product Map(Persistence.EntityFramework.Entities.Product dbProduct)
        {
            return new Models.Products.Product()
            {
                ProductId = dbProduct.ProductId,
                ProductName = dbProduct.ProductName,
                UnitPrice = dbProduct.UnitPrice,
            };
        }
        
        private static Persistence.EntityFramework.Entities.Product Map(Models.Products.Product modelProduct)
        {
            return new Persistence.EntityFramework.Entities.Product()
            {
                ProductId = modelProduct.ProductId,
                ProductName = modelProduct.ProductName,
                UnitPrice = modelProduct.UnitPrice,
            };
        }

        private static Persistence.EntityFramework.Entities.Product Map(Persistence.EntityFramework.Entities.Product productDb, Models.Products.Product modelProduct)
        {
            productDb.ProductName = modelProduct.ProductName;
            productDb.UnitPrice = modelProduct.UnitPrice;

            return productDb;
        }
    }
}