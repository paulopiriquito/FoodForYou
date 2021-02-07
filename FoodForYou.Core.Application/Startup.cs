using System;
using FoodForYou.Core.Application.Implementations.Customer;
using FoodForYou.Core.Application.Implementations.Employee;
using FoodForYou.Core.Application.Implementations.Order;
using FoodForYou.Core.Application.Implementations.Product;
using FoodForYou.Core.Application.Implementations.Shipper;
using FoodForYou.Core.Application.Implementations.Shopping;
using FoodForYou.Core.Application.Implementations.User;
using FoodForYou.Core.Application.ServiceContracts;
using FoodForYou.Persistence.EntityFramework;
using FoodForYou.Persistence.EntityFramework.Context;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodForYou.Core.Application
{
    public static class Startup
    {
        public static void AddApplication(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddPersistence(configuration);
            
            serviceCollection.AddScoped<ICustomerService, CustomerService>();
            serviceCollection.AddScoped<IShipperService, ShipperService>();
            serviceCollection.AddScoped<IEmployeeService, EmployeeService>();
            serviceCollection.AddScoped<IOrderService, OrderService>();
            serviceCollection.AddScoped<IProductService, ProductService>();
            serviceCollection.AddScoped<IShoppingService, ShoppingService>();
            serviceCollection.AddScoped<IUserService, UserService>();
            serviceCollection.AddSingleton<IShoppingService, ShoppingService>();
        }
    }
}