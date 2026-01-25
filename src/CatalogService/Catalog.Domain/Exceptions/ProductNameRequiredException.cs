using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Catalog.Domain.Exceptions
{
    public class ProductNameRequiredException : DomainException
    {
        public ProductNameRequiredException()
            : base("Product name is required.") { }
    }
}
