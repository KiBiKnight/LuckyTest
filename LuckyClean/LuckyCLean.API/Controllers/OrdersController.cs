using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCLean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(ProcessOrderRequest request)
        {
            var result = await _orderService.ProcessOrderAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}
