using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Catalog.Domain.Exceptions
{
    public class InvalidProductCategoryException : DomainException
    {
        public InvalidProductCategoryException()
            : base("Product name is required.") { }
    }
}
