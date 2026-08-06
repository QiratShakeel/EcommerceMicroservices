using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Repositories
{
    public class ProductCommandRepository : IProductCommandRepository
    {
        private readonly CatalogDbContext _context;

        public ProductCommandRepository(CatalogDbContext context)
        {
            _context = context;
        }

        

        public async Task AddAsync(Product product, CancellationToken ct)
        {
            await _context.Products.AddAsync(product, ct);
            // Do NOT call SaveChangesAsync here; handled by UnitOfWork
        }

        public Task UpdateAsync(Product product)
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

        

        

        

    }
}
