using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Persistence.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Core.Application.Implementations.Shipper
{
    public class ShipperService : IShipperService
    {
        protected readonly IStoreDbContext DbContext;

        public ShipperService(IStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<Models.Shippers.Shipper>> GetAll(CancellationToken cancellationToken = default)
        {
            var shippers = await DbContext.Shippers
                .Select(shipper => Map(shipper))
                .ToListAsync(cancellationToken);

            return shippers;
        }

        public async Task<Models.Shippers.Shipper> GetId(int id, CancellationToken cancellationToken = default)
        {
            var shipper = await DbContext.Shippers
                .Where(x=>x.ShipperId == id)
                .Select(productDb => Map(productDb))
                .FirstOrDefaultAsync(cancellationToken);

            return shipper;
        }

        public async Task<Models.Shippers.Shipper> Create(Models.Shippers.Shipper model, CancellationToken cancellationToken = default)
        {
            var shipper = await DbContext.Shippers.AddAsync(Map(model), cancellationToken);

            return Map(shipper.Entity);
        }

        public async Task<Models.Shippers.Shipper> Edit(Models.Shippers.Shipper model, CancellationToken cancellationToken = default)
        {
            var shipperDb = await DbContext.Shippers
                .FirstOrDefaultAsync(x=>x.ShipperId == model.ShipperId, cancellationToken);

            shipperDb = DbContext.Shippers.Update(Map(shipperDb, model)).Entity;

            await DbContext.SaveChangesAsync(cancellationToken);
            
            return Map(shipperDb);
        }

        public async Task Delete(Models.Shippers.Shipper model, CancellationToken cancellationToken = default)
        {
            var shipperDb = await DbContext.Shippers
                .Where(x=> x.ShipperId == model.ShipperId)
                .FirstOrDefaultAsync(cancellationToken);

            DbContext.Shippers.Remove(shipperDb);

            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Shippers
                .AnyAsync(x => x.ShipperId == id, cancellationToken);
        }
        
        private static Models.Shippers.Shipper Map(Persistence.EntityFramework.Entities.Shipper dbShipper)
        {
            return new Models.Shippers.Shipper()
            {
                ShipperId = dbShipper.ShipperId,
                CompanyName = dbShipper.CompanyName
            };
        }
        
        private static Persistence.EntityFramework.Entities.Shipper Map(Models.Shippers.Shipper modelShipper)
        {
            return new Persistence.EntityFramework.Entities.Shipper()
            {
                ShipperId = modelShipper.ShipperId,
                CompanyName = modelShipper.CompanyName
            };
        }

        private static Persistence.EntityFramework.Entities.Shipper Map(Persistence.EntityFramework.Entities.Shipper shipperDb, Models.Shippers.Shipper modelShipper)
        {
            shipperDb.CompanyName = modelShipper.CompanyName;

            return shipperDb;
        }
    }
}