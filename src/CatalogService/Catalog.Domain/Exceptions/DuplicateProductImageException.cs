using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Catalog.Domain.Exceptions
{
    public class DuplicateProductImageException : DomainException
    {
        public DuplicateProductImageException()
            : base("Duplicate image URL.") { }
    }
}
