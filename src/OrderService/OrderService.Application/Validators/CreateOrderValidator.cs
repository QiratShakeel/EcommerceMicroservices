using FluentValidation;
using Ecommerce.Orders.Application.Commands;

namespace Ecommerce.Orders.Application.Validators
{
    public class CreateOrderValidator
        : OrderBaseValidator<CreateOrderCommandWithUser>
    {
        public CreateOrderValidator()
        {
            // extra rules later (items, address, etc.)
        }
    }
}
