using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Identity.Domain.Exceptions
{
    public class UserNameTooLongException : DomainException
    {
        public UserNameTooLongException()
        : base("User Name Too Long") { }
    }
}