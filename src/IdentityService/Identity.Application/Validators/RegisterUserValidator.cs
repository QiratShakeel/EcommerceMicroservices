using Ecommerce.Identity.Application.Commands;
using Ecommerce.Identity.Application.Interfaces;
using FluentValidation;

namespace Ecommerce.Identity.Application.Validators
{
    public class RegisterUserValidator : UserBaseValidator<RegisterUserCommand> 
    {
        public RegisterUserValidator()
        {
            RuleFor(x => x.name)
                .NotEmpty()
                .MaximumLength(100)
                .WithMessage("Name is required and max 100 chars");


        }
    }
}