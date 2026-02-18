using LuckyClean.Application.DTOs;
using LuckyClean.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LuckyCLean.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> ProcessOrder(ProcessOrderRequest request)
        {
            var result = await _orderService.ProcessOrderAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{customerId}/history")]
        public async Task<IActionResult> GetOrderHistory(int customerId)
        {
            var orders = await _orderService.GetOrderHistoryAsync(customerId);
            return Ok(orders);
        }
    }
}
