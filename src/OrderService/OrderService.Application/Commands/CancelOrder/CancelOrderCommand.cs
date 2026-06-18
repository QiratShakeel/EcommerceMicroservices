using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Results;

namespace Ecommerce.Orders.Application.Commands
{
    public record CancelOrderCommand(Guid OrderId, string Reason) : ICommand<Result>;
}