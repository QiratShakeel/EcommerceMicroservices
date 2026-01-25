//using BuildingBlocks.EventBus.Abstractions;
//using Ecommerce.Orders.Domain.Enums;
//using Ecommerce.Orders.Infrastructure.Persistence.Context;

//namespace Ecommerce.Payment.Infrastructure.Messaging.Consumers
//{
//    public class PaymentFailedConsumer
//        : IIntegrationEventHandler<PaymentFailedIntegrationEvent>
//    {
//        private readonly OrdersDbContext _context;

//        public PaymentFailedConsumer(OrdersDbContext context)
//        {
//            _context = context;
//        }

//        public async Task Handle(PaymentFailedIntegrationEvent @event)
//        {
//            var order = await _context.Orders
//                .FindAsync(@event.OrderId);

//            if (order == null)
//                return; // or throw if you prefer strict consistency

//            if (order.Status == OrderStatus.Cancelled)
//                return; // idempotency protection

//            order.Cancel();   // domain logic inside aggregate
//            await _context.SaveChangesAsync();
//        }
//    }
//}
