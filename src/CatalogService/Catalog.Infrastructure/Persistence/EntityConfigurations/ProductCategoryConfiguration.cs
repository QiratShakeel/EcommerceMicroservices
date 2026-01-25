using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.Entities;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Configurations
{
    public class ProductCategoryConfiguration
        : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");

            // Composite Primary Key
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId }).HasName("PK_ProductCategory");

            builder.Property(pc => pc.ProductId).IsRequired();
            builder.Property(pc => pc.CategoryId).IsRequired();
            // Optional: configure relationships (if needed)
            builder.HasOne<Product>()
                   .WithMany("_productCategories") // private field in Product
                   .HasForeignKey(pc => pc.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Category>() // if you have Category entity
                   .WithMany()
                   .HasForeignKey(pc => pc.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
