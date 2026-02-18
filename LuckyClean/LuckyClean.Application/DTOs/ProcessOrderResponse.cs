namespace LuckyClean.Application.DTOs
{
    public class ProcessOrderResponse
    {
        public bool IsSuccess { get; set; }
        public decimal Total { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
