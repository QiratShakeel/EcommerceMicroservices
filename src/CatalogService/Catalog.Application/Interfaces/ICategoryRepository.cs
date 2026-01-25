using Ecommerce.Catalog.Domain.Entities;
namespace Ecommerce.Catalog.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetAsync(int id, CancellationToken ct);
        Task<List<Category>> GetAllAsync(CancellationToken ct);
        Task AddAsync(Category category, CancellationToken ct);
        Task UpdateAsync(Category category, CancellationToken ct);
        Task<bool> HasProductsAsync(int categoryId, CancellationToken ct);
        Task DeleteAsync(Category category);
    }
}