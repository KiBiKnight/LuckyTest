using LuckyClean.Application.Interfaces;
using LuckyClean.Domain.Entities;
using LuckyClean.Domain.Enums;
using LuckyClean.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace LuckyClean.Infrastructure.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;
        private readonly PaymentSettings _settings;

        public PaymentService(HttpClient httpClient, IOptions<PaymentSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<PaymentResult> ProcessPaymentAsync(PaymentMethod method, decimal amount)
        {
            try
            {
                string response;

                switch (method)
                {
                    case PaymentMethod.CreditCard:
                        response = await _httpClient.GetStringAsync(
                            $"{_settings.CreditCardUrl}?amount={amount}");
                        break;

                    case PaymentMethod.PayPal:
                        response = await _httpClient.GetStringAsync(
                            $"{_settings.PayPalUrl}?amount={amount}");
                        break;

                    case PaymentMethod.BankTransfer:
                        return PaymentResult.Pending();

                    default:
                        return PaymentResult.Failure(400, "Invalid payment method");
                }

                if (response.Contains("success"))
                    return PaymentResult.Success();

                return PaymentResult.Failure(400, "Payment failed");
            }
            catch (Exception ex)
            {
                return PaymentResult.Failure(500, ex.Message);
            }
        }
    }
}
