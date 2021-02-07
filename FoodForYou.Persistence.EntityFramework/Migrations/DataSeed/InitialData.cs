using System;
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
            modelBuilder.Entity<Product>().HasData(
                new Product() {ProductId = 1, ProductName = "Olive Oil", UnitPrice = (float) 1.20},
                new Product() {ProductId = 2, ProductName = "Cookies", UnitPrice = (float) 1.00},
                new Product() {ProductId = 3, ProductName = "Coke", UnitPrice = (float) 2.20},
                new Product() {ProductId = 4, ProductName = "Apple", UnitPrice = (float) 0.15}
                );
        }

        private static void SeedEmployees(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                    new Employee() {
                        EmployeeId = 1,
                        Title = "CEO",
                        TitleOfCourtesy = "Mrs.",
                        Address = "Baker Street 221B",
                        BirthDate = new DateTime(1990, 12, 10),
                        City = "London",
                        Country = "United Kingdom",
                        FullName = "Scarlet Holmes",
                        HomePhone = "+351 245234234",
                        PostalCode = "432-32335",
                        Region = "West End"
                    }
                );
        }

        private static void SeedShippers(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Shipper>().HasData(new Shipper[] { });
        }
    }
}
