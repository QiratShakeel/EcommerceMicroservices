using Ecommerce.Catalog.Domain.ValueObjects;
using MediatR;
namespace Ecommerce.Catalog.Application.Commands
{
    public record DeleteProductCommand(
    Guid prodId
    ) : IRequest<bool>;

}