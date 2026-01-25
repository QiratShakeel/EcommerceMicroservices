using FluentValidation;
using Ecommerce.Orders.Application.Commands;

namespace Ecommerce.Orders.Application.Validators
{
    public class CreateOrderValidator
        : OrderBaseValidator<CreateOrderCommand>
    {
        public CreateOrderValidator()
        {
            // extra rules later (items, address, etc.)
        }
    }
}
