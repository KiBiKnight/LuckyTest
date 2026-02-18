using LuckyClean.Domain.Enums;

namespace LuckyClean.Application.DTOs
{
    public class ProcessOrderRequest
    {
        public int CustomerId { get; set; }
        public string CustomerEmail { get; set; } = string.Empty;
        public List<string> Items { get; set; } = new();
        public PaymentMethod PaymentMethod { get; set; }
        public decimal Discount { get; set; }
    }
}
