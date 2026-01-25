using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Catalog.Domain.Exceptions
{
    public class ProductPublishException : DomainException
    {
        public ProductPublishException(string message) : base(message) { }
    }
}
