using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Catalog.Domain.Exceptions
{
    public class DuplicateProductCategoryException : DomainException
    {
        public DuplicateProductCategoryException()
            : base("Category already assigned to product.") { }
    }
}
