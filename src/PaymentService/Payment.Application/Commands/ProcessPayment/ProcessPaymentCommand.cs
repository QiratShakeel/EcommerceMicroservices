using BuildingBlocks.Shared.Results;
using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Payment.Application.Commands
{
    public record ProcessPaymentCommand(Guid OrderId, Guid CustomerId, decimal Amount)
    : ICommand<Result>;
}