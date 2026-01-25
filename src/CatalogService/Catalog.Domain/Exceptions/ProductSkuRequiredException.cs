using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Catalog.Domain.Exceptions
{
    public class ProductSkuRequiredException : DomainException
    {
        public ProductSkuRequiredException()
            : base("Product SKU is required.") { }
    }
}
