using LuckyClean.Domain.Entities;

namespace LuckyClean.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByNameAsync(string name);
        Task<Product> AddAsync(Product product);
        Task<List<Product>> GetAllAsync();
    }
}
