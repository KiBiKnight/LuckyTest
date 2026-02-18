using LuckyClean.Application.DTOs;
using LuckyClean.Domain.Entities;

namespace LuckyClean.Application.Interfaces
{
    public interface IOrderService
    {
        Task<ProcessOrderResponse> ProcessOrderAsync(ProcessOrderRequest request);
        Task<List<string>> GetOrderHistoryAsync(int customerId);
    }
}
