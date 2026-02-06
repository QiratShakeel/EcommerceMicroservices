using MediatR;
using BuildingBlocks.Shared.Results;
namespace Ecommerce.Payment.Application.Commands
{
    public record ProcessPaymentCommand(Guid OrderId,decimal Amount)
    : IRequest<Result>;
}