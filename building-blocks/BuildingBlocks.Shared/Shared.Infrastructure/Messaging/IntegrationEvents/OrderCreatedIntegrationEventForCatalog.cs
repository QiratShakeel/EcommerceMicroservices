using BuildingBlocks.EventBus.Abstractions;
using BuildingBlocks.Shared.Infrastructure.Dto;
using System.Text.Json.Serialization;

public class OrderCreatedIntegrationEventForCatalog : IIntegrationEvent
{
    [JsonInclude]
    public Guid OrderId { get; set; }
    [JsonInclude]
    public List<CreateOrderItemDto> OrderItems { get; set; } 
}