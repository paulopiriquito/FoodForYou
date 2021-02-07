using FoodForYou.Persistence.EntityFramework.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FoodForYou.Persistence.EntityFramework
{
    public static class Startup
    {
        public static void AddPersistence(this IServiceCollection serviceCollection, IConfiguration configuration)
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
        }
    }
}