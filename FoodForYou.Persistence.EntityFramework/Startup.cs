using System;
using FoodForYou.Persistence.EntityFramework.Context;
using FoodForYou.Persistence.EntityFramework.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodForYou.Persistence.EntityFramework
{
    public static class Startup
    {
        public static void AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration, Action<IdentityOptions> identityOptions = null)
        {
            serviceCollection.AddDbContext<IStoreDbContext, StoreDbContext>(builder =>
            {
                if (configuration.GetSection("DbSettings").GetValue<bool>("UseMemory"))
                {
                    builder.UseInMemoryDatabase(
                        configuration.GetConnectionString("StoreDatabase")
                    );
                }
                else
                {
                    builder.UseSqlServer(
                        configuration.GetConnectionString("StoreDatabase")
                    );
                }
            });

            if (identityOptions != null)
            {
                serviceCollection.AddIdentity(configuration, identityOptions);
            }
        }

        public static void AddIdentity(this IServiceCollection serviceCollection, IConfiguration configuration, Action<IdentityOptions> identityOptions)
        {
            serviceCollection.AddDefaultIdentity<IdentityUser>(identityOptions)
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddRoles<Customer>()
                .AddRoles<Employee>();
        }
    }
}