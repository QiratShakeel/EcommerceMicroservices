using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Infrastructure.Dto;
using BuildingBlocks.Shared.Results;

namespace Ecommerce.Catalog.Application.Commands
{
    public record ReduceInventoryCommand(IEnumerable<CreateOrderItemDto> items) : ICommand<Result>;    
}