using Ecommerce.Catalog.Domain.Aggregates;
using System.Threading;
using System.Collections.Generic;
namespace Ecommerce.Catalog.Application.Interfaces
{
    // Defines the operations needed for the Product Aggregate Root
    public interface IProductCommandRepository
    {
        // The domain defines the contract:

        Task AddAsync(Product product, CancellationToken ct);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        // Maybe a specific domain requirement:
        //Task ReduceInventoryAsync(Guid productId, int quantity);        
    }
}