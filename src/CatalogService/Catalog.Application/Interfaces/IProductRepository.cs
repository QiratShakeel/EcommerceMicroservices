using Ecommerce.Catalog.Domain.Aggregates;
using System.Threading;
using System.Collections.Generic;
namespace Ecommerce.Catalog.Application.Interfaces
{
    // Defines the operations needed for the Product Aggregate Root
    public interface IProductRepository
    {
        // The domain defines the contract:
        Task<Product?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct);
        Task<List<Product>> GetByIdsAsync(IEnumerable<Guid> ids, CancellationToken ct);
        Task AddAsync(Product product, CancellationToken ct);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        // Maybe a specific domain requirement:
        //Task ReduceInventoryAsync(Guid productId, int quantity);
        Task<bool> IsSkuUniqueAsync(string sku, CancellationToken ct);
    }
}