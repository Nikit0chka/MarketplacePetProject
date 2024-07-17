using Microsoft.EntityFrameworkCore;
using Products.DAL.ContextModels;

namespace Products.DAL.Data
{
    public class ProductsDbContext : DbContext
    {
        public required DbSet<ProductContext> Products { get; set; }

        public ProductsDbContext() : base()
        {
            Database.EnsureDeleted(); // гарантируем, что бд будет удалена
            Database.EnsureCreated(); // гарантируем, что бд будет создана
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductContext>().HasData(new ProductContext
            {
                Id = 1,
                Name = "Ручка",
                Count = 4,
                Price = 15.0,
            }, new ProductContext
            {
                Id = 2,
                Name = "Карандаш",
                Count = 7,
                Price = 10.0,
            }, new ProductContext
            {
                Id = 3,
                Name = "Линейка",
                Count = 12,
                Price = 23.5,
            });
        }
    }
}
