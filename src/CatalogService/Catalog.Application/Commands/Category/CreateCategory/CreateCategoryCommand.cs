using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Application.Commands.CreateCategory
{
    public record CreateCategoryCommand(
    string Name,
    string? Desc,
    Guid? ParentId
    ) : ICommand<Guid>;

}