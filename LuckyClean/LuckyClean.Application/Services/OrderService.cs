using FluentValidation;
using LuckyClean.Application.DTOs;
using LuckyClean.Application.Interfaces;
using LuckyClean.Domain.Entities;
using LuckyClean.Domain.Enums;
using LuckyClean.Domain.Interfaces;

namespace LuckyClean.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPaymentService _paymentService;
        private readonly IEmailService _emailService;
        private readonly IValidator<ProcessOrderRequest> _validator;

        public OrderService(
            IOrderRepository orderRepository,
            IProductRepository productRepository,
            IPaymentService paymentService,
            IEmailService emailService,
            IValidator<ProcessOrderRequest> validator)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _paymentService = paymentService;
            _emailService = emailService;
            _validator = validator;
        }

        public async Task<ProcessOrderResponse> ProcessOrderAsync(ProcessOrderRequest request)
        {
            var validationResult = await _validator.ValidateAsync(request);
            if (!validationResult.IsValid)
            {
                return new ProcessOrderResponse
                {
                    IsSuccess = false,
                    Message = string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage))
                };
            }

            decimal total = 0;
            foreach (var itemName in request.Items)
            {
                var product = await _productRepository.GetByNameAsync(itemName);
                if (product == null)
                {
                    return new ProcessOrderResponse
                    {
                        IsSuccess = false,
                        Message = $"Product '{itemName}' not found"
                    };
                }
                total += product.Price;
            }

            if (request.Discount > 0)
            {
                total -= total * request.Discount;
            }

            var paymentResult = await _paymentService.ProcessPaymentAsync(request.PaymentMethod, total);
            if (!paymentResult.IsSuccess)
            {
                return new ProcessOrderResponse
                {
                    IsSuccess = false,
                    Message = "Payment failed"
                };
            }

            var order = new Order
            {
                CustomerId = request.CustomerId,
                Total = total,
                Status = OrderStatus.Completed,
                PaymentMethod = request.PaymentMethod
            };
            await _orderRepository.AddAsync(order);

            try
            {
                await _emailService.SendOrderConfirmationAsync(request.CustomerEmail, total);
            }
            catch (Exception)
            {
                return new ProcessOrderResponse
                {
                    IsSuccess = true,
                    Total = total,
                    Message = $"Order processed but email failed. Total: ${total}"
                };
            }

            return new ProcessOrderResponse
            {
                IsSuccess = true,
                Total = total,
                Message = $"Order processed successfully. Total: ${total}"
            };
        }

        public async Task<List<string>> GetOrderHistoryAsync(int customerId)
        {
            var orders = await _orderRepository.GetByCustomerIdAsync(customerId);
            return orders.Select(o => $"Order #{o.Id} - ${o.Total} - {o.Status}").ToList();
        }
    }
}
