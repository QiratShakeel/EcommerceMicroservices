using Ecommerce.Catalog.Application.Commands;
using FluentValidation;

namespace Ecommerce.Catalog.Application.Validators
{
    public class CreateProductValidator : ProductBaseValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.SKU).NotEmpty().Length(5, 20).Matches(@"^[a-zA-Z0-9]+$").WithMessage("SKU must be uppercase letters and digits only");
        }
    }
}

