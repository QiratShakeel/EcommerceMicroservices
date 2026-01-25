using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Context
{
    public class CatalogDbContextFactory : IDesignTimeDbContextFactory<CatalogDbContext>
    {
        public CatalogDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CatalogDbContext>();
            optionsBuilder.UseSqlServer("Server=DESKTOP-O1E4QQ7\\SQLEXPRESS;Database=CatalogDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new CatalogDbContext(optionsBuilder.Options);
        }
    }
}
