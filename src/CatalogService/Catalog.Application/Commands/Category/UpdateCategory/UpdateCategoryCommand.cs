using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Application.Commands
{
    public record UpdateCategoryCommand(
    Guid CategoryId,
    string Name,
    string? Desc,
    Guid? ParentId
    ) : ICommand<Guid>;

}