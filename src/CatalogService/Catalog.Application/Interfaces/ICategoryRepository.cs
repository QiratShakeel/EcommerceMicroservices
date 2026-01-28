using Ecommerce.Catalog.Domain.Entities;
namespace Ecommerce.Catalog.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetAsync(Guid id, CancellationToken ct);
        Task<List<Category>> GetAllAsync(CancellationToken ct);
        Task AddAsync(Category category, CancellationToken ct);
        Task UpdateAsync(Category category, CancellationToken ct);
        Task<bool> HasProductsAsync(Guid categoryId, CancellationToken ct);
        Task DeleteAsync(Category category);
    }
}