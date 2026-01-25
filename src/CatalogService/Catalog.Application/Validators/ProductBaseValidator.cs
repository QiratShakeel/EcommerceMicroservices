using FluentValidation;

namespace Ecommerce.Catalog.Application.Validators
{
    public class ProductBaseValidator<T> : AbstractValidator<T> where T : IProductRequest
    {
        public ProductBaseValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Desc).MaximumLength(100);
            //RuleFor(x => x.BrandId).GreaterThan(0);
            //RuleFor(x => x.currencyCode).NotEmpty().Length(3).Matches(@"^[A-Z]{3}$");
        }
    }
}

//| Rule | Example | Notes |
//| ----------------------- | ----------------------------------------------------------------------------------------- | ----------------------------- |
//| Not null / empty | `RuleFor(x => x.Name).NotEmpty();`                      | Checks string not null/empty  |
//| Greater than            | `RuleFor(x => x.Price).GreaterThan(0);`          | Works for numeric types       |
//| Greater or equal | `RuleFor(x => x.Quantity).GreaterThanOrEqualTo(1);`     |                               |
//| Less than / LessOrEqual | `RuleFor(x => x.Age).LessThan(100);`             |                               |
//| Length | `RuleFor(x => x.Name).Length(3, 50);`                             | Min and max length for string |
//| Matches / Regex | `RuleFor(x => x.SKU).Matches(@"^[A-Z0-9]+$");`           |                               |
//| Email | `RuleFor(x => x.Email).EmailAddress();`                            | Built -in email validation |
//| Collection | `RuleForEach(x => x.Tags).NotEmpty();`                        | Validate items in a list |
//| Custom rule | `RuleFor(x => x.Price).Must(p => p % 5 == 0).WithMessage("Price must be multiple of 5");` | Custom logic |
