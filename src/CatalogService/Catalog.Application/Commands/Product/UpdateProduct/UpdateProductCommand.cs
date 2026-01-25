using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using BuildingBlocks.Shared.Results;

namespace Ecommerce.Catalog.Application.Commands
{
    public record UpdateProductCommand(
        Guid ProductId,
        string Name,
        decimal Price,
        string Desc,
        List<int>? CategoryIds,
        List<ProductImage>? Images
    ) : IRequest<Result<Guid>>, IProductRequest;

}