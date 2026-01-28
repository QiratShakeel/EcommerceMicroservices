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
            builder.HasKey(pc => new { pc.ProductId, pc.CategoryId}).HasName("PK_ProductCategory");

            // Optional: configure relationships (if needed)
            builder.HasOne(pc=>pc.Product)
                   .WithMany(pc=>pc.Categories) // private field in Product
                   .HasForeignKey(pc => pc.ProductId).OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(pc=>pc.Category) // if you have Category entity
                   .WithMany()
                   .HasForeignKey(pc => pc.CategoryId).OnDelete(DeleteBehavior.Restrict);

            //builder.Metadata.FindNavigation(nameof(ProductCategory.Product))?.SetPropertyAccessMode(PropertyAccessMode.Field);
            //builder.Metadata.FindNavigation(nameof(ProductCategory.Category))?.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(pc => pc.Category).UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(pc => pc.Product).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
