using LuckyClean.Domain.Entities;
using LuckyClean.Domain.Enums;

namespace LuckyClean.Application.Interfaces
{
    internal interface IPaymentService
    {
        Task<PaymentResult> ProcessPaymentAsync(PaymentMethod method, decimal amount);
    }
}
