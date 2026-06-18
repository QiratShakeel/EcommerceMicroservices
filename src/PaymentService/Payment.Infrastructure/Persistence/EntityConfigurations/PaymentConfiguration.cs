using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ecommerce.Payment.Domain.Aggregates;
using Ecommerce.Payment.Domain.Entities;
using Ecommerce.Payment.Domain.Enums;

namespace Ecommerce.Payment.Infrastructure.Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
    {
        public void Configure(EntityTypeBuilder<PaymentEntity> builder)
        {
            // Table
            builder.ToTable("Payments");

            // Primary Key
            builder.HasKey(p => p.Id);

            // Properties
            builder.Property(p => p.OrderId).IsRequired();
            builder.Property(p => p.CustomerId).IsRequired();
            builder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");
            builder.Property(p => p.Status).IsRequired().HasConversion<string>(); // Enum to string

            // Owned Transactions collection
            builder.OwnsMany(p => p.Transactions, t =>
            {
                t.ToTable("Transactions");  // Separate table for clarity
                t.WithOwner().HasForeignKey("PaymentId"); // FK to Payment
                t.HasKey("Id"); // Shadow PK

                t.Property(tr => tr.Amount).IsRequired().HasColumnType("decimal(18,2)");
                t.Property(tr => tr.Provider).IsRequired().HasMaxLength(50);
                t.Property(tr => tr.ReferenceId).IsRequired().HasMaxLength(100);
                t.Property(tr => tr.Status).IsRequired().HasConversion<string>();
                t.Property(tr => tr.CreatedAt).IsRequired();
            });
            builder.Navigation(p => p.Transactions).UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}