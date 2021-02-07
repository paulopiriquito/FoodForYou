using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Core.Models.Exceptions;
using FoodForYou.Core.Models.Orders;
using FoodForYou.Core.Models.Relational;
using FoodForYou.Persistence.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Core.Application.Implementations.Order
{
    public class OrderService : IOrderService
    {
        protected readonly IStoreDbContext DbContext;

        public OrderService(IStoreDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<IEnumerable<OrderDetails>> GetAll(CancellationToken cancellationToken = default)
        {
            var orders = await DbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .ToListAsync(cancellationToken);

            var ordersModel = new List<OrderDetails>();

            foreach (var order in orders)
            {
                ordersModel.Add(await GetDetailModel(order, cancellationToken));
            }

            return ordersModel;
        }

        public async Task<OrderDetails> GetId(int id, CancellationToken cancellationToken = default)
        {
            if (id == null)
            {
                throw new ArgumentException("Invalid Order Identifier", nameof(id));
            }
            
            var order = await DbContext.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee)
                .Include(o => o.Shipper)
                .FirstOrDefaultAsync(x=> x.OrderId.Equals(id),cancellationToken);

            return await GetDetailModel(order, cancellationToken);
        }

        private async Task<OrderDetails> GetDetailModel(Persistence.EntityFramework.Entities.Order order, CancellationToken cancellationToken)
        {
            if (order == null)
            {
                return null;
            }
            
            var details = await DbContext.OrderDetails
                .Include(d => d.Product)
                .Where(d=> d.OrderId == order.OrderId)
                .ToListAsync(cancellationToken);

            var products = details.Select(detail => new OrderProduct()
            {
                Product = new Models.Products.Product()
                {
                    ProductId = detail.ProductId,
                    ProductName = detail.Product.ProductName,
                    UnitPrice = detail.Product.UnitPrice,
                },
                Quantity = detail.Quantity
            });

            return new OrderDetails()
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                Customer = new Models.Costumers.Customer()
                {
                    CustomerId = order.CustomerId,
                    Title = order?.Customer?.Title,
                    FullName = order?.Customer?.FullName
                },
                Employee = new Models.Employees.Employee()
                {
                    EmployeeId = order.EmployeeId,
                    FullName = order?.Employee?.FullName,
                    Title = order?.Employee?.Title,
                    TitleOfCourtesy = order?.Employee?.TitleOfCourtesy
                },
                Products = products,
                Shipper = new Models.Shippers.Shipper(){ShipperId = order.ShipperId, CompanyName = order.Shipper.CompanyName}
            };
        }

        public async Task<OrderDetails> Create(OrderDetails model, CancellationToken cancellationToken = default)
        {
            var order = await DbContext.Orders.AddAsync(new Persistence.EntityFramework.Entities.Order()
            {
                OrderDate = DateTime.Now,
                CustomerId = model.Customer.CustomerId,
                EmployeeId = model?.Employee?.EmployeeId,
                ShipperId = model.Shipper.ShipperId
            }, cancellationToken);

            model.OrderId = order.Entity.OrderId;

            foreach (var modelProduct in model.Products)
            {
                await DbContext.OrderDetails.AddAsync(new Persistence.EntityFramework.Entities.OrderDetails()
                {
                    ProductId = modelProduct.Product.ProductId,
                    OrderId = order.Entity.OrderId,
                    Quantity = modelProduct.Quantity,
                }, cancellationToken);
            }

            await DbContext.SaveChangesAsync(cancellationToken);

            return model;
        }

        public async Task<OrderDetails> Edit(OrderDetails model, CancellationToken cancellationToken = default)
        {
            var order = await DbContext.Orders.FirstOrDefaultAsync(x=>x.OrderId == model.OrderId, cancellationToken);

            if (order != null)
            {
                order.CustomerId = model.Customer.CustomerId;
                order.EmployeeId = model.Employee.EmployeeId;
                order.ShipperId = model.Shipper.ShipperId;

                DbContext.Orders.Update(order);

                var orderDetails = DbContext.OrderDetails.Where(x => x.OrderId == model.OrderId);

                foreach (var orderDetail in orderDetails)
                {
                    foreach (var modelProduct in model.Products)
                    {
                        if (orderDetail.ProductId != modelProduct.Product.ProductId) continue;
                    
                        orderDetail.Quantity = modelProduct.Quantity;
                        DbContext.OrderDetails.Update(orderDetail);
                    }
                }
            
                await DbContext.SaveChangesAsync(cancellationToken);
            }
            
            return model;
        }

        public async Task Delete(OrderDetails model, CancellationToken cancellationToken = default)
        {
            var order = await DbContext.Orders.FirstOrDefaultAsync(x=>x.OrderId == model.OrderId, cancellationToken);

            if (order != null)
            {
                DbContext.OrderDetails.RemoveRange(DbContext.OrderDetails.Where(x=>x.OrderId == order.OrderId));
                DbContext.Orders.Remove(order);
                
                await DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<OrderDetails> CreateFromCart(CostumerCart cart, Models.Shippers.Shipper shipper, CancellationToken cancellationToken = default)
        {
            if (cart.Costumer == null || cart.Costumer.CustomerId == default)
            {
                throw new CustomerRegistrationException();
            }
            
            return await Create(new OrderDetails()
            {
                Customer = cart.Costumer,
                OrderDate = DateTime.Now,
                Shipper = shipper,
                Products = cart.CartProducts
            }, cancellationToken);
        }

        public async Task<OrderDetails> AddManager(Models.Orders.Order order, Models.Employees.Employee manager, CancellationToken cancellationToken = default)
        {
            var orderDb = 
                await DbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == order.OrderId, cancellationToken);
            if (orderDb == null) 
                throw new Exception("Order not found");
            
            var managerDb =
                await DbContext.Employees.FirstOrDefaultAsync(x => x.EmployeeId == manager.EmployeeId, cancellationToken);

            if (managerDb == null) 
                throw new Exception("Employee not found");
                
            orderDb.EmployeeId = managerDb.EmployeeId;
            DbContext.Orders.Update(orderDb);
            await DbContext.SaveChangesAsync(cancellationToken);
            return await GetDetailModel(orderDb, cancellationToken);
        }

        public async Task<OrderDetails> EditDetails(OrderDetails order, CancellationToken cancellationToken = default)
        {
            var orderDb = await DbContext.Orders.FirstOrDefaultAsync(x => x.OrderId == order.OrderId, cancellationToken);
            var orderDetails = DbContext.OrderDetails.Where(x => x.OrderId == order.OrderId);

            foreach (var orderDetail in orderDetails)
            {
                foreach (var modelProduct in order.Products)
                {
                    if (orderDetail.ProductId != modelProduct.Product.ProductId) continue;
                    
                    orderDetail.Quantity = modelProduct.Quantity;
                    DbContext.OrderDetails.Update(orderDetail);
                }
            }
            
            await DbContext.SaveChangesAsync(cancellationToken);

            return await GetDetailModel(orderDb, cancellationToken);
        }
        
        public async Task<bool> Exists(int id, CancellationToken cancellationToken = default)
        {
            return await DbContext.Orders.AnyAsync(x => x.OrderId.Equals(id), cancellationToken);
        }
    }
}