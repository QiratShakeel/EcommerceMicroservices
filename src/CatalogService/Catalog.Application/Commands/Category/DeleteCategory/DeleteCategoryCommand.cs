using Ecommerce.Catalog.Domain.ValueObjects;
using MediatR;
namespace Ecommerce.Catalog.Application.Commands
{
    public record DeleteCategoryCommand(
    int categoryId
    ) : IRequest<bool>;

}