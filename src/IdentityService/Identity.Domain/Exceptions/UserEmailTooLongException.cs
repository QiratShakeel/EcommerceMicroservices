using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Identity.Domain.Exceptions
{
    public class UserEmailTooLongException : DomainException
    {
        public UserEmailTooLongException()
        : base("User Email is too Long") { }
    }
}