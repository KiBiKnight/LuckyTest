using LuckyClean.Domain.Entities;

namespace LuckyClean.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<List<Order>> GetByCustomerIdAsync(int customerId);
    }
}
