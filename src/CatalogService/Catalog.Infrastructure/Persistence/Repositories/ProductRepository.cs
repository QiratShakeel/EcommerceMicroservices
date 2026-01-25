using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.Id == id, ct);
        }

        public async Task AddAsync(Product product, CancellationToken ct)
        {
            await _context.Products.AddAsync(product, ct);
            // Do NOT call SaveChangesAsync here; handled by UnitOfWork
        }

        public Task UpdateAsync(Product product, CancellationToken ct)
        {
            _context.Products.Update(product);
            return Task.CompletedTask;
            // SaveChangesAsync handled by UnitOfWork
        }

        public Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            return Task.CompletedTask;
            // SaveChangesAsync handled by UnitOfWork
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Products.ToListAsync(ct);
        }

        public async Task<bool> IsSkuUniqueAsync(string sku, CancellationToken ct)
        {
            // Returns true if SKU does NOT exist
            return !await _context.Products.AnyAsync(p => p.SKU == sku, ct);
        }
    }
}
