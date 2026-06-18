using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Domain.Aggregates;
namespace Ecommerce.Orders.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(OrderEntity order, CancellationToken cancellationToken);
        Task<OrderEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task UpdateAsync(OrderEntity order);
    }
}