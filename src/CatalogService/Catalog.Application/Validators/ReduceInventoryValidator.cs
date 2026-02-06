using Ecommerce.Catalog.Application.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Application.Validators
{
    public class ReduceInventoryValidator: AbstractValidator<ReduceInventoryCommand>
    {
        public ReduceInventoryValidator()
        {
            RuleForEach(i => i.items).ChildRules(items =>
            {
                items.RuleFor(x => x.ProductId).NotEmpty().WithMessage("ProductId is required");
                items.RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Amount must be greater than zero");
            });
        }
    }
}
