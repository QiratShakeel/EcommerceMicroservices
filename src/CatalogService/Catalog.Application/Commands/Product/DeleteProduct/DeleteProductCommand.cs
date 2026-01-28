using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Domain.ValueObjects;
using MediatR;
namespace Ecommerce.Catalog.Application.Commands
{
    public record DeleteProductCommand(
    Guid prodId
    ) : ICommand<bool>;

}