using Ecommerce.Catalog.Application.Dto;
using Ecommerce.Catalog.Domain.Aggregates;
namespace Ecommerce.Catalog.Application.Interfaces
{
    public interface IProductQueries
    {
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<ProductDto?>> GetAllAsync(CancellationToken ct);
        Task<List<Product?>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct);
        Task<List<ProductDto?>> GetFeaturedProductsAsync();
        Task<List<ProductDto?>> GetNewProductsAsync();
        Task<List<ProductDto?>> GetProductsByCategoryAsync();
        Task<bool> IsSkuUniqueAsync(string sku, CancellationToken ct);
    }
}