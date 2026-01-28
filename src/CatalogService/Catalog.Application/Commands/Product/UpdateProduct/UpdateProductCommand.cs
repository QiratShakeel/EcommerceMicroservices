using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using BuildingBlocks.Shared.Results;
using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Catalog.Application.Commands
{
    public record UpdateProductCommand(
        Guid ProductId,
        string Name,
        decimal Price,
        string? Desc,
        List<Guid>? CategoryIds,
        List<ProductImage>? Images
    ) : ICommand<Result<Guid>>, IProductRequest;

}