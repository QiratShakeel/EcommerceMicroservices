using BuildingBlocks.Shared.Behaviors.Transaction;
using BuildingBlocks.Shared.Grpc.Catalog;
using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Infrastructure.Dto;
using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Domain.Aggregates;
using Grpc.Core;
using MediatR;

namespace Ecommerce.Orders.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly CatalogGrpc.CatalogGrpcClient _catalogClient;
        private readonly IUnitOfWork _uow;
        private readonly IOrderRepository _repository;
        private readonly DomainEventDispatcher _dispatcher;

        public OrderService(CatalogGrpc.CatalogGrpcClient catalogClient, IUnitOfWork uow, IOrderRepository repository, DomainEventDispatcher dispatcher)
        {
            _catalogClient = catalogClient;
            _uow = uow;
            _repository = repository;
            _dispatcher = dispatcher;
        }

        public async Task<List<OrderItemDto>> ValidateAndGetProductDetails(List<CreateOrderItemDto> items)
        {
            var result = new List<OrderItemDto>();
            foreach (var item in items)
            {
                try
                {
                    // gRPC Call (Transaction ke bahar - SAFE!)
                    var product = await _catalogClient.GetProductByIdAsync(new GetProductByIdRequest { ProductId = item.ProductId.ToString() });
                    result.Add(new OrderItemDto { ProductId = Guid.Parse(product.ProductId), Price = (decimal)product.Price, Quantity = item.Quantity });
                }
                catch (RpcException ex)
                {
                    // Yahan aap log kar sakte hain ya specific business error throw kar sakte hain
                    throw new Exception($"Catalog Service Error: {ex.Status.Detail}");
                }
            }
            return result;
        }

        public async Task<Guid> PlaceOrderAsync(Guid customerId, List<OrderItemDto> validatedItems, CancellationToken cancellationToken)
        {
            // DB Transaction (Only for Writes - FAST!)
            return await _uow.ExecuteWithTransactionAsync(async () =>
            {
                var order = new OrderEntity(customerId);
                foreach (var item in validatedItems)
                {
                    order.AddItem(item.ProductId, item.Price, item.Quantity);
                }
                order.Confirm();
                await _repository.AddAsync(order, cancellationToken);
                return order.Id;
            },  _dispatcher); // Dispatcher yahan pass karein
        }
    }
}