using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ecommerce.Identity.Infrastructure.Persistence.Context
{
    public class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        public IdentityDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<IdentityDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-O1E4QQ7\\SQLEXPRESS;Database=IdentityDb;User Id=sa;Password=123;TrustServerCertificate=True;");

            return new IdentityDbContext(optionsBuilder.Options);
        }
    }
}
