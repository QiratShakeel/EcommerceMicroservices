using BuildingBlocks.Shared.Infrastructure.Dto;
namespace Ecommerce.Orders.Application.Commands
{
    public record CreateOrderRequest(
        List<CreateOrderItemDto> Items);
}
