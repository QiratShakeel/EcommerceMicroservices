
using Ecommerce.Identity.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ecommerce.Identity.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.PasswordHash)
                .IsRequired()
                .HasMaxLength(500)
                .IsUnicode(false);

            builder.Property(x => x.CreatedDate)
                .IsRequired();

            builder.Property(x => x.isActive)
                .IsRequired();

            // Roles Many-to-Many
            builder
                .HasMany(u => u.Roles)
                .WithMany()
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoles",
                    j => j.HasOne<Role>()
                          .WithMany()
                          .HasForeignKey("RoleId"),
                    j => j.HasOne<User>()
                          .WithMany()
                          .HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("UserRoles");
                    });

            //// 🔹 Configure private field access
            //builder.Metadata.FindNavigation(nameof(User.Roles))
            //       .SetPropertyAccessMode(PropertyAccessMode.Field);

            // 🔹 Optional: Default values, computed columns, etc.
            builder.Property(u => u.isActive).HasDefaultValue(true);
        }
    }
}
