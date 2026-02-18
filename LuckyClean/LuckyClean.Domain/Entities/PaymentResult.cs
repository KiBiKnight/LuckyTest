using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LuckyClean.Domain.Entities
{
    public class PaymentResult
    {
        public bool IsSuccess { get; set; }
        public string Status { get; set; } = string.Empty;
        public int ResponseCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
