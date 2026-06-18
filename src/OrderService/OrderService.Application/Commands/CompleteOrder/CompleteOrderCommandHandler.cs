using BuildingBlocks.Shared.Results;
using Ecommerce.Orders.Application.Commands;
using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Domain.Enums;
using Grpc.Core;
using MediatR;

namespace Ecommerce.Orders.Application.EventsHandlers
{
    public class CompleteOrderCommandHandler : IRequestHandler<CompleteOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        public CompleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Result> Handle(CompleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(request.OrderId, cancellationToken);
            if (order == null)
                return Result.Failure("Order not found");
            if (order.Status == OrderStatus.Completed)
                return Result.Success(); // already processed
            if (order.Status != OrderStatus.Confirmed)
                return Result.Failure("Invalid state transition");
            order.Complete();
            await _orderRepository.UpdateAsync(order);
            return Result.Success();
        }
    }
}