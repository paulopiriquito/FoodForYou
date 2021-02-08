using FoodForYou.Persistence.EntityFramework.Entities;
using FoodForYou.Persistence.EntityFramework.Migrations.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Persistence.EntityFramework.Context
{
    public class StoreDbContext : DbContext, IStoreDbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> contextOptions) : base(contextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().Property(x => x.UnitPrice).HasColumnType("money");
            modelBuilder.Entity<User>().HasKey(x => x.UserId);
            modelBuilder.Entity<User>().HasIndex(x => x.Username);
            modelBuilder.Entity<User>().HasIndex(x => x.Email);

            modelBuilder.Seed();
            base.OnModelCreating(modelBuilder);
        }

        public void EnsureCreated()
        {
            Database.EnsureCreated();
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Shipper> Shippers { get; set; }
    }
}