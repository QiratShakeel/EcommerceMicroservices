using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Catalog.Domain.Entities;

namespace Ecommerce.Catalog.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CatalogDbContext _context;

        public CategoryRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Category category, CancellationToken ct)
        {
            await _context.Categories.AddAsync(category);
        }

        public Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            return Task.CompletedTask;
        }

        public async Task<List<Category>> GetAllAsync(CancellationToken ct)
        {
            return await _context.Categories.ToListAsync(ct);
        }

        public async Task<Category?> GetAsync(Guid id, CancellationToken ct)
        {
            return await _context.Categories.FirstOrDefaultAsync(x=>x.Id==id,ct);
        }

        public async Task<bool> HasChildrenAsync(Guid categoryId, CancellationToken token)
        {
            return await _context.Categories.AnyAsync(x => x.ParentCategoryId == categoryId, token);
        }

        public async Task<bool> HasProductsAsync(Guid categoryId, CancellationToken ct)
        {
            return await _context.ProductCategories.AnyAsync(x=>x.CategoryId==categoryId,ct);
        }

        public Task UpdateAsync(Category category, CancellationToken ct)
        {
            _context.Categories.Update(category);
            return Task.CompletedTask;
        }
    }
}
