using Ecommerce.Identity.Application.Interfaces;
using FluentValidation;

namespace Ecommerce.Identity.Application.Validators
{
    public class UserBaseValidator<T>: AbstractValidator<T> where T : IUserRequest
    {
        public UserBaseValidator()
        {
            RuleFor(x=>x.email).NotEmpty().MaximumLength(100).Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            RuleFor(x => x.password).NotEmpty().MinimumLength(8).MaximumLength(128).WithMessage("Password must be 8-128 chars");
        }
    }
}