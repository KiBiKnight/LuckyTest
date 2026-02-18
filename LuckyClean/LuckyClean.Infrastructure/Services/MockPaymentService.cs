using LuckyClean.Application.Interfaces;
using LuckyClean.Domain.Entities;
using LuckyClean.Domain.Enums;

namespace LuckyClean.Infrastructure.Services
{
    public class MockPaymentService : IPaymentService
    {
        public Task<PaymentResult> ProcessPaymentAsync(PaymentMethod method, decimal amount)
        {
            return method switch
            {
                PaymentMethod.CreditCard => Task.FromResult(PaymentResult.Success()),
                PaymentMethod.PayPal => Task.FromResult(PaymentResult.Success()),
                PaymentMethod.BankTransfer => Task.FromResult(PaymentResult.Pending()),
                _ => Task.FromResult(PaymentResult.Failure(400, "Invalid payment method"))
            };
        }
    }
}
