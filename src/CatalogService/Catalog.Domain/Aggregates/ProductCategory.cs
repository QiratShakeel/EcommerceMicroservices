using Ecommerce.Catalog.Domain.Entities;
namespace Ecommerce.Catalog.Domain.Aggregates
{
    public class ProductCategory
    {
        public Guid ProductId { get; private set; }
        public Guid CategoryId { get; private set; }

        public Product Product { get; private set; }
        public Category Category { get; private set; }

        private ProductCategory() { } // EF

        public ProductCategory(Guid productId, Guid categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;           
        }
    }
}