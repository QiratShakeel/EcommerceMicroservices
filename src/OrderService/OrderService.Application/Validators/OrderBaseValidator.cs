using FluentValidation;
using Ecommerce.Orders.Application.Interfaces;

namespace Ecommerce.Orders.Application.Validators
{
    public class OrderBaseValidator<T> : AbstractValidator<T>
        where T : IOrderRequest
    {
        public OrderBaseValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
        }
    }
}
