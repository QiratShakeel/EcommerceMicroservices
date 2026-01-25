using MediatR;
namespace Ecommerce.Catalog.Application.Commands
{
    public record UpdateCategoryCommand(
    int CategoryId,
    string Name,
    string Desc,
    int? ParentId
    ) : IRequest<int>;

}