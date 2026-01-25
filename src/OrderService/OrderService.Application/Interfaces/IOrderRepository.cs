using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Domain.Aggregates;
namespace Ecommerce.Orders.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(OrderEntity order);
        Task<OrderEntity?> GetByIdAsync(Guid id);
    }
}