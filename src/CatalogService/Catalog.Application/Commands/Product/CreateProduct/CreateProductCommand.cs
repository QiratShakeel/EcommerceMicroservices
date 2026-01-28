using MediatR;
using Ecommerce.Catalog.Domain.ValueObjects;
using BuildingBlocks.Shared.Results;
using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Application.Commands
{
    public record CreateProductCommand(
    string Name,
    string SKU,
    decimal Price,
    string? Desc= null,
    List<Guid>? CategoryIds= null,
    List<ProductImage>? Images = null
    ) : ICommand<Result<Guid>>, IProductRequest;

}