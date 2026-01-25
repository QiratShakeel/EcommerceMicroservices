using BuildingBlocks.EventBus.Abstractions;

namespace Ecommerce.Catalog.Infrastructure.Messaging.IntegrationEvents
{
    public class ProductCreatedIntegrationEvent : IIntegrationEvent
    {
        public Guid ProductId { get; }
        public string Name { get; }
        public string SKU { get; }

        public ProductCreatedIntegrationEvent(Guid productId, string name, string sku)
        {
            ProductId = productId;
            Name = name;
            SKU = sku;
        }
    }
}