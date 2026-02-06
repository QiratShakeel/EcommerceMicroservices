using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Domain.Aggregates;
using BuildingBlocks.Shared.Results;
using MediatR;
using Ecommerce.Orders.Application.Commands;
using AutoMapper;
using BuildingBlocks.Shared.Grpc.Catalog;

namespace Ecommerce.Orders.Application.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<Guid>>
    {
        private readonly IOrderService _orderService;

        public CreateOrderCommandHandler(IOrderService orderService) => _orderService = orderService;

        public async Task<Result<Guid>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. External Call (No DB Lock)
                var validatedItems = await _orderService.ValidateAndGetProductDetails(request.Items);

                // 2. DB Work (Atomic Transaction)
                var orderId = await _orderService.PlaceOrderAsync(request.CustomerId, validatedItems);

                return Result<Guid>.Success(orderId);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.Message);
            }
        }
    }
}