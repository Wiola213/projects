using CosmeticsStoreDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CosmeticsStoreDB.Data
{
    public class CosmeticsContext :DbContext
    {
        
        public CosmeticsContext(DbContextOptions<CosmeticsContext> options)
            : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel { ID = 1, Name = "Krem do twarzy 50ml", Price = 200.00m, AvailableQuantity = 10 },
                new ProductModel { ID = 2, Name = "Balsam do ciała 500 ml", Price = 50.00m, AvailableQuantity = 20 },
                new ProductModel { ID = 3, Name = "Dezodorant 1szt.", Price = 50.00m, AvailableQuantity = 30 },
                new ProductModel { ID = 4, Name = "Żel pod prysznic 750 ml", Price = 20.00m, AvailableQuantity = 5 },
                new ProductModel { ID = 5, Name = "Odżywka do włosów 200ml", Price = 30.00m, AvailableQuantity = 7 }
            );
        }
    }
}

