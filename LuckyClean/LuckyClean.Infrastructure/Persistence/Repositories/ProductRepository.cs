using LuckyClean.Domain.Entities;
using LuckyClean.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LuckyClean.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByNameAsync(string name)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }
    }
}
