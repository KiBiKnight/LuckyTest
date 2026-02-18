using LuckyClean.Application.Interfaces;

namespace LuckyClean.Infrastructure.Services
{
    public class MockEmailService : IEmailService
    {
        public Task SendOrderConfirmationAsync(string email, decimal total)
        {
            Console.WriteLine($"[MOCK EMAIL] To: {email} | Total: ${total}");
            return Task.CompletedTask;
        }
    }
}
