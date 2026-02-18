using LuckyClean.Domain.Enums;

namespace LuckyClean.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(PaymentMethod method, decimal amount);
    }
}
