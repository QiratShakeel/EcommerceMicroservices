using Ecommerce.Catalog.Domain.Entities;
namespace Ecommerce.Catalog.Domain.Aggregates
{
    public class ProductCategory
    {
        public Guid ProductId { get; private set; }
        public int CategoryId { get; private set; }

        public Product Product { get; set; }
        public Category Category { get; set; }

        private ProductCategory() { } // EF

        public ProductCategory(Guid productId, int categoryId)
        {
            ProductId = productId;
            CategoryId = categoryId;
        }
    }
}