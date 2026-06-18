using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Infrastructure.Dto;

namespace BuildingBlocks.Shared.Infrastructure.Messaging.IntegrationEvents
{
    public record OrderCompletedIntegrationEvent(List<CreateOrderItemDto> OrderItems): IIntegrationEvent;
}