using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Identity.Domain.Exceptions
{
    public class UserEmailRequiredException : DomainException
    {
        public UserEmailRequiredException()
        : base("User Email is Required") { }
    }
}