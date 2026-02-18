using LuckyClean.Application.Interfaces;
using LuckyClean.Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace LuckyClean.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;

        public EmailService(IOptions<EmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendOrderConfirmationAsync(string email, decimal total)
        {
            using var smtp = new SmtpClient(_settings.SmtpHost, _settings.SmtpPort);
            smtp.Credentials = new NetworkCredential(_settings.SenderEmail, _settings.SenderPassword);
            smtp.EnableSsl = _settings.EnableSsl;

            var mail = new MailMessage();
            mail.From = new MailAddress(_settings.SenderEmail);
            mail.To.Add(email);
            mail.Subject = "Order Confirmation";
            mail.Body = $"Your order has been placed. Total: ${total}";

            await smtp.SendMailAsync(mail);
        }
    }
}
