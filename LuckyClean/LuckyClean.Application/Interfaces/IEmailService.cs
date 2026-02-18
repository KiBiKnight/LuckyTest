namespace LuckyClean.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendOrderConfirmationAsync(string email, decimal total);
    }
}
