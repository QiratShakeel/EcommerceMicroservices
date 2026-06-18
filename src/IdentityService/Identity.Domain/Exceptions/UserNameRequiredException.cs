using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Identity.Domain.Exceptions
{
    public class UserNameRequiredException: DomainException
    {
        public UserNameRequiredException()
        : base("User Name is Required") { }
    }
}