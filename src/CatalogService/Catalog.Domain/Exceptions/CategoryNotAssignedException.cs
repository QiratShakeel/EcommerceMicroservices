using BuildingBlocks.Shared.Exceptions;

namespace Ecommerce.Catalog.Domain.Exceptions
{
    public class CategoryNotAssignedException : DomainException
    {
        public CategoryNotAssignedException()
            : base("Category not assigned to this product.") { }
    }
}
