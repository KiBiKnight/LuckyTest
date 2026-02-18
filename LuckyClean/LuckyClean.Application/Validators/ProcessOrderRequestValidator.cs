using FluentValidation;
using LuckyClean.Application.DTOs;

namespace LuckyClean.Application.Validators
{
    public class ProcessOrderRequestValidator : AbstractValidator<ProcessOrderRequest>
    {
        public ProcessOrderRequestValidator()
        {
            RuleFor(x => x.CustomerId)
                .GreaterThan(0).WithMessage("Customer ID must be greater than 0");

            RuleFor(x => x.CustomerEmail)
                .NotEmpty().WithMessage("Customer email is required")
                .EmailAddress().WithMessage("Invalid email address");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Order must contain at least one item");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum().WithMessage("Invalid payment method");

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 1).WithMessage("Discount must be between 0 and 1");
        }
    }
}
