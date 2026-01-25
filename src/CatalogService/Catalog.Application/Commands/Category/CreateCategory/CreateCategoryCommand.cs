using MediatR;
namespace Ecommerce.Catalog.Application.Commands.CreateCategory
{
    public record CreateCategoryCommand(
    string Name,
    string Desc,
    int? ParentId
    ) : IRequest<int>;

}