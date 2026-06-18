using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Identity.Domain.Exceptions
{
    public class UserEmailPatternException : DomainException
    {
        public UserEmailPatternException()
        : base("User Email pattern is invalid") { }
    }
}