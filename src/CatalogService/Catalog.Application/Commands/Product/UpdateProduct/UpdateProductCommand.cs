using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Results;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.ValueObjects;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Catalog.Application.Commands
{
    public record UpdateProductCommand(
        Guid ProductId,
        string Name,
        decimal Price,
        int stock,
        string? Desc,
        List<Guid>? CategoryIds,
        List<IFormFile>? Images
    ) : ICommand<Result<Guid>>, IProductRequest;

}