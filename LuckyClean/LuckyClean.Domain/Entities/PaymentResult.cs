namespace LuckyClean.Domain.Entities
{
    public class PaymentResult
    {
        public bool IsSuccess { get; private set; }
        public int ResponseCode { get; private set; }
        public string? ErrorMessage { get; private set; }

        public static PaymentResult Success(int code = 202)
            => new() { IsSuccess = true, ResponseCode = code };

        public static PaymentResult Pending()
            => new() { IsSuccess = true, ResponseCode = 200 };

        public static PaymentResult Failure(int code, string error)
            => new() { IsSuccess = false, ResponseCode = code, ErrorMessage = error };
    }
}
