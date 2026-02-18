using LuckyClean.Domain.Entities;
using LuckyClean.Domain.Enums;

namespace LuckyClean.Application.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(PaymentMethod method, decimal amount);
    }
}
