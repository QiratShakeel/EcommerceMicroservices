using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.ValueObjects;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            // Scalar properties
            builder.Property(p => p.Name).IsRequired().HasMaxLength(200);

            builder.Property(p => p.Description).HasMaxLength(1000).IsRequired(false); 

            builder.Property(p => p.SKU).IsRequired().HasMaxLength(50);

            builder.OwnsOne(p => p.Price, m =>
            {
                m.Property(x => x.Amount).HasColumnName("Price").HasPrecision(18, 2);
            });

            builder.Property(p => p.Status).HasConversion<int>(); // Enum to int

            builder.OwnsOne(p => p.Inventory, inv =>
            {
                inv.Property(x => x.StockQuantity).IsRequired();
                inv.Property(x => x.ReservedQuantity).IsRequired(false); //false krne hai isko wh khrh ha int property isrequired null nh hskti
                inv.Property(x => x.WarehouseLocation).HasMaxLength(200).IsRequired(false);
            });

            builder.Property(p => p.CreatedDate).IsRequired().HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.UpdatedDate);

            //builder.HasMany<ProductCategory>("_productCategories").WithOne().HasForeignKey(pc => pc.ProductId).OnDelete(DeleteBehavior.Cascade);
            builder.OwnsMany(p => p.Images, img =>
            {
                img.ToTable("ProductImages"); // optional but recommended

                img.WithOwner().HasForeignKey("ProductId");

                img.Property(i => i.Url).IsRequired().HasMaxLength(500);

                img.Property(i => i.AltText).HasMaxLength(200).IsRequired(false);

                img.Property(i => i.FileType).IsRequired().HasMaxLength(10);

                img.Property<int>("Id");
                img.HasKey("Id");
            });

            //builder.Navigation("_productCategories").UsePropertyAccessMode(PropertyAccessMode.Field);
            builder.Navigation(p => p.Images)?.UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}


            //builder.Property<List<int>>("_categoryIds")
            //   .HasColumnName("CategoryIds")
            //   .HasConversion(
            //       v => string.Join(",", (List<int>)v),
            //       v => v.Split(",", StringSplitOptions.RemoveEmptyEntries)
            //       .Select(int.Parse)
            //       .ToList()
            //   );

            // Collections: Images
            //builder.HasMany(typeof(ProductImage), "_images") // private field mapping
            //       .WithOne() // assuming ProductImage has no nav property
            //       .HasForeignKey("ProductId") // shadow FK
            //       .OnDelete(DeleteBehavior.Cascade);

            //builder.Metadata
            //       .FindNavigation("_categoryIds")?
            //       .SetPropertyAccessMode(PropertyAccessMode.Field);

            // Optional: Ignore backing fields from EF Core shadow