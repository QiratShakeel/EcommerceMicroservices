using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Orders.Domain.Aggregates;
namespace Ecommerce.Orders.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
    {
        public void Configure(EntityTypeBuilder<OrderEntity> builder)
        {
            builder.HasKey(o => o.Id);

            builder.OwnsMany(o => o.OrderItems, i =>
            {
                i.WithOwner().HasForeignKey("OrderId");

                i.Property<Guid>("Id");
                i.HasKey("Id");

                i.Property(x => x.ProductId).IsRequired();
                i.Property(x => x.UnitPrice).HasPrecision(18, 2);
                i.Property(x => x.Quantity).IsRequired();
            });
            builder.Navigation(o => o.OrderItems).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}