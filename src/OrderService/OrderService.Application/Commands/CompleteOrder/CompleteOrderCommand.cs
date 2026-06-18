using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Results;

namespace Ecommerce.Orders.Application.Commands
{
    public record CompleteOrderCommand(Guid OrderId) : ICommand<Result>;
}