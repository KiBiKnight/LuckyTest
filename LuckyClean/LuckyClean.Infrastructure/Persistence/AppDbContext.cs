using LuckyClean.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LuckyClean.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Order> Orders => Set<Order>();
        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Widget", Price = 9.99m },
                new Product { Id = 2, Name = "Gadget", Price = 19.99m },
                new Product { Id = 3, Name = "Thing", Price = 29.99m }
            );
        }
    }
}
