using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Catalog.Application.Commands
{
    public record DeleteCategoryCommand(
    Guid categoryId
    ) : ICommand<bool>;

}