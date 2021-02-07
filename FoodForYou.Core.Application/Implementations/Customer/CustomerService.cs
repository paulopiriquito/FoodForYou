using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Core.Models.Costumers;
using FoodForYou.Persistence.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Core.Application.Implementations.Customer
{
    public class CustomerService : ICustomerService
    {
        protected readonly IStoreDbContext DbContext;

        public CustomerService(IStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        async Task<IEnumerable<Models.Costumers.Customer>> ICrudService<Models.Costumers.Customer>.GetAll(CancellationToken cancellationToken)
        {
            var customers = await DbContext.Customers
                .Select(customer => new Models.Costumers.Customer()
                {
                    CustomerId = customer.CustomerId, 
                    FullName = customer.FullName, 
                    Title = customer.FullName
                })
                .ToListAsync(cancellationToken);

            return customers;
        }

        async Task<IEnumerable<CustomerDetails>> ICustomerService.GetAll(CancellationToken cancellationToken)
        {
            var customers = await DbContext.Customers
                .Select(customer => Map(customer))
                .ToListAsync(cancellationToken);

            return customers;
        }

        public async Task<Models.Costumers.Customer> GetId(int id, CancellationToken cancellationToken = default)
        {
            var customerDb = await DbContext.Customers
                .Where(x=> x.CustomerId == id)
                .Select(customer => new Models.Costumers.Customer()
                {
                    CustomerId = customer.CustomerId,
                    FullName = customer.FullName,
                    Title = customer.FullName
                })
                .FirstOrDefaultAsync(cancellationToken);

            return customerDb;
        }

        public async Task Delete(Models.Costumers.Customer model, CancellationToken cancellationToken = default)
        {
            var customerDb = await DbContext.Customers
                .Where(x=> x.CustomerId == model.CustomerId)
                .FirstOrDefaultAsync(cancellationToken);

            DbContext.Customers.Remove(customerDb);

            await DbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Customers
                .AnyAsync(x => x.CustomerId == id, cancellationToken);
        }

        public async Task<CustomerDetails> GetDetails(Models.Costumers.Customer costumer, CancellationToken cancellationToken = default)
        {
            return await GetDetails(costumer?.CustomerId, cancellationToken);
        }

        public async Task<CustomerDetails> GetDetails(int? costumerId, CancellationToken cancellationToken = default)
        {
            var customerDetail = await DbContext.Customers
                .Where(x=> x.CustomerId == costumerId)
                .Select(customer => Map(customer))
                .FirstOrDefaultAsync(cancellationToken);

            return customerDetail;
        }

        public async Task<CustomerDetails> Create(CustomerDetails costumer, CancellationToken cancellationToken = default)
        {
            var customerDb = await DbContext.Customers.AddAsync(Map(costumer), cancellationToken);

            await DbContext.SaveChangesAsync(cancellationToken);

            return await GetDetails(customerDb.Entity.CustomerId, cancellationToken);
        }

        public async Task<CustomerDetails> Edit(CustomerDetails costumer, CancellationToken cancellationToken = default)
        {
            var customerDb =
                await DbContext.Customers.FirstOrDefaultAsync(x => x.CustomerId == costumer.CustomerId, cancellationToken);

            var customerUpdated = DbContext.Customers.Update(Map(customerDb, costumer));

            await DbContext.SaveChangesAsync(cancellationToken);

            return Map(customerUpdated.Entity);
        }
        
        public async Task<Models.Costumers.Customer> Create(Models.Costumers.Customer model, CancellationToken cancellationToken = default)
        {
            var customerDb = await DbContext.Customers.AddAsync(Map(model), cancellationToken);

            await DbContext.SaveChangesAsync(cancellationToken);

            return await GetDetails(customerDb.Entity.CustomerId, cancellationToken);
        }

        public async Task<Models.Costumers.Customer> Edit(Models.Costumers.Customer model, CancellationToken cancellationToken = default)
        {
            if (model is CustomerDetails customerDetails)
            {
                return await Edit(customerDetails, cancellationToken);
            }
            throw new NotSupportedException();
        }

        private static CustomerDetails Map(Persistence.EntityFramework.Entities.Customer dbcostumer)
        {
            return new CustomerDetails()
            {
                CustomerId = dbcostumer.CustomerId,
                FullName = dbcostumer.FullName,
                Title = dbcostumer.Title,
                Address = dbcostumer.Address,
                City = dbcostumer.City,
                Country = dbcostumer.Country,
                Fax = dbcostumer.Fax,
                HomePhone = dbcostumer.HomePhone,
                PostalCode = dbcostumer.PostalCode,
                Region = dbcostumer.Region,
            };
        }
        
        private static Persistence.EntityFramework.Entities.Customer Map(CustomerDetails modelCustomer)
        {
            return new Persistence.EntityFramework.Entities.Customer()
            {
                CustomerId = modelCustomer.CustomerId,
                FullName = modelCustomer.FullName,
                Title = modelCustomer.Title,
                Address = modelCustomer.Address,
                City = modelCustomer.City,
                Country = modelCustomer.Country,
                Fax = modelCustomer.Fax,
                HomePhone = modelCustomer.HomePhone,
                PostalCode = modelCustomer.PostalCode,
                Region = modelCustomer.Region,
            };
        }
        
        private static Persistence.EntityFramework.Entities.Customer Map(Models.Costumers.Customer modelCustomer)
        {
            return new Persistence.EntityFramework.Entities.Customer()
            {
                CustomerId = modelCustomer.CustomerId,
                FullName = modelCustomer.FullName,
                Title = modelCustomer.Title,
            };
        }
        
        private static Persistence.EntityFramework.Entities.Customer Map(Persistence.EntityFramework.Entities.Customer customerDb, CustomerDetails modelCustomer)
        {
            customerDb.FullName = modelCustomer.FullName;
            customerDb.Title = modelCustomer.Title;
            customerDb.Address = modelCustomer.Address;
            customerDb.City = modelCustomer.City;
            customerDb.Country = modelCustomer.Country;
            customerDb.Fax = modelCustomer.Fax;
            customerDb.HomePhone = modelCustomer.HomePhone;
            customerDb.PostalCode = modelCustomer.PostalCode;
            customerDb.Region = modelCustomer.Region;

            return customerDb;
        }
    }
}