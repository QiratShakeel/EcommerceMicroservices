using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.Events;
using MediatR;

public class ProductDeletedDomainEventHandler
    : INotificationHandler<ProductDeletedDomainEvent>
{
    private readonly IFileService _fileService;

    public ProductDeletedDomainEventHandler(
        IFileService fileService)
    {
        _fileService = fileService;
    }

    public async Task Handle(
        ProductDeletedDomainEvent notification,
        CancellationToken cancellationToken)
    {
        foreach (var imageUrl in notification.ImageUrls)
        {
            await _fileService.DeleteAsync(imageUrl);
        }
    }
}