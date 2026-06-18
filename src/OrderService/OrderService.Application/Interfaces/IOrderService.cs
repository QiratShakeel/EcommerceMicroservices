using BuildingBlocks.Shared.Infrastructure.Dto;
using Ecommerce.Orders.Application.Dto;

namespace Ecommerce.Orders.Application.Interfaces
{
    public interface IOrderService
    {
        // Ye method gRPC calls handle karega (Transaction se pehle)
        Task<List<OrderItemDto>> ValidateAndGetProductDetails(List<CreateOrderItemDto> items);

        // Ye method sirf DB transaction handle karega
        Task<Guid> PlaceOrderAsync(Guid customerId, List<OrderItemDto> validatedItems, CancellationToken cancellationToken);
    }
}