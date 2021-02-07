using FoodForYou.Persistence.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;

namespace FoodForYou.Persistence.EntityFramework.Migrations.DataSeed
{
    public static class InitialData
    {
        public static ModelBuilder Seed(this ModelBuilder modelBuilder)
        {
            SeedProducts(modelBuilder);
            SeedEmployees(modelBuilder);
            SeedShippers(modelBuilder);

            return modelBuilder;
        }


        private static void SeedProducts(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new[] { 
                new Product() {ProductId = 1, ProductName = "Azeite", UnitPrice = 1.20},
                new Product() {ProductId = 2,  ProductName = "Bolachas", UnitPrice = 1.00},
                new Product() {ProductId = 3,  ProductName = "Cola", UnitPrice = 2.20},
                new Product() {ProductId = 4,  ProductName = "Maçã", UnitPrice = 0.15},
            });
        }

        private static void SeedEmployees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee[] { });
        }

        private static void SeedShippers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipper>().HasData(new Shipper[] { });
        }
    }
}
