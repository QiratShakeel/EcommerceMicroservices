using MediatR;
using Ecommerce.Catalog.Domain.ValueObjects;
using BuildingBlocks.Shared.Results;
namespace Ecommerce.Catalog.Application.Commands
{
    public record CreateProductCommand(
    string Name,
    string SKU,
    decimal Price,
    string? Desc= null,
    List<int>? CategoryIds= null,
    List<ProductImage>? Images = null
    ) : IRequest<Result<Guid>>, IProductRequest;

}