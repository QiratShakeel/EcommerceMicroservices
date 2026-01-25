using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Catalog.Domain.Entities;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Categories");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.Description)
               .HasMaxLength(1000);

            //// Self-referencing one-to-many (Parent -> Children)
            //builder.HasMany(typeof(Category), "_children") // private field for children
            //       .WithOne()                               // no navigation in child
            //       .HasForeignKey("ParentCategoryId")       // shadow FK column
            //       .OnDelete(DeleteBehavior.Restrict);      // prevent cascade delete if needed

            builder.HasMany(c => c.Children)
               .WithOne()
               .HasForeignKey(c => c.ParentCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

            // Optional: access private field
            //builder.Metadata.FindNavigation("_children")
            //       ?.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.Navigation(c => c.Children)
               .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
