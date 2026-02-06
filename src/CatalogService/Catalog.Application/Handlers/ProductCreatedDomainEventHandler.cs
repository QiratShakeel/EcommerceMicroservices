using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Domain.Events;
using MailKit.Net.Smtp;
using MimeKit;
using MediatR;
using Microsoft.Extensions.Logging;
using MailKit;

namespace Ecommerce.Catalog.Application.EventsHandlers
{
    public class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
    {
        private readonly ILogger<ProductCreatedDomainEventHandler> _logger;
            //private readonly IMailService _emailService;

        public ProductCreatedDomainEventHandler(ILogger<ProductCreatedDomainEventHandler> logger)
        {
            _logger = logger;
            //_emailService = emailService;
        }
        public Task Handle(ProductCreatedDomainEvent domainEvent, CancellationToken token)
        {
            _logger.LogInformation(
                "Product {ProductId} price changed from {OldPrice} to {NewPrice}",
                domainEvent.ProductId,
                domainEvent.Name,
                domainEvent.SKU
            );
            //await _emailService.SendEmailAsync(
            //"admin@company.com",
            //$"New Product Created: {domainEvent.Name}",
            //$"Product '{domainEvent.Name}' (SKU: {domainEvent.SKU}) was created with ID {domainEvent.ProductId}."
            //);
            // Other logic: send email, update cache, etc.

            return Task.CompletedTask;
        }
    }
}
