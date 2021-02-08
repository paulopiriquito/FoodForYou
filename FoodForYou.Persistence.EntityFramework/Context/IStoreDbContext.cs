using System.Threading;
using System.Threading.Tasks;
using FoodForYou.Persistence.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Persistence.EntityFramework.Context
{
    public interface IStoreDbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());

        public void EnsureCreated();
    }
}
