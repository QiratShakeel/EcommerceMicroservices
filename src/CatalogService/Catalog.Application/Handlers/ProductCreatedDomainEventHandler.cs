using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Catalog.Application.EventsHandlers
{
    public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly ILogger<ProductCreatedDomainEventHandler> _logger;

        public ProductCreatedDomainEventHandler(ILogger<ProductCreatedDomainEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken token)
        {
            _logger.LogInformation(
                "Product {ProductId} price changed from {OldPrice} to {NewPrice}",
                domainEvent.ProductId,
                domainEvent.Name,
                domainEvent.SKU
            );

            // Other logic: send email, update cache, etc.

            return Task.CompletedTask;
        }
    }
}
