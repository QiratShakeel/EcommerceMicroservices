//using BuildingBlocks.EventBus.Abstractions;
//using Ecommerce.Payment.Infrastructure.Messaging.IntegrationEvents;

//namespace Ecommerce.Payment.Infrastructure.Messaging.Consumers
//{
//    public class PaymentSucceededConsumer
//    : IIntegrationEventHandler<PaymentSucceededIntegrationEvent>
//    {
//        private readonly OrdersDbContext _context;

//        public PaymentSucceededConsumer(OrdersDbContext context)
//        {
//            _context = context;
//        }

//        public async Task Handle(PaymentSucceededIntegrationEvent @event)
//        {
//            var order = await _context.Orders.FindAsync(@event.OrderId);
//            order!.MarkPaid();
//            await _context.SaveChangesAsync();
//        }
//    }
//}