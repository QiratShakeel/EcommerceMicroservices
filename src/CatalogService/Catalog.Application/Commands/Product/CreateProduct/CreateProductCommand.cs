using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Results;
using Ecommerce.Catalog.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;
namespace Ecommerce.Catalog.Application.Commands
{
    public record CreateProductCommand(
    string Name,
    string SKU,
    decimal Price,
    int stock, 
    string? Desc= null,
    List<Guid>? CategoryIds= null,
    List<IFormFile>? Images = null
    ) : ICommand<Result<Guid>>, IProductRequest;

}