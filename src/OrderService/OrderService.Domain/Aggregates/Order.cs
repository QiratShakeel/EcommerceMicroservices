using BuildingBlocks.Shared.Infrastructure;
using BuildingBlocks.Shared.Results;
using Ecommerce.Orders.Domain.Enums;
using Ecommerce.Orders.Domain.Events;

namespace Ecommerce.Orders.Domain.Aggregates
{
    public class OrderEntity : Entity, IAggregateRoot
    {
        private readonly List<OrderItem> _orderItems = new();

        public Guid CustomerId { get; private set; }
        public DateTime OrderDate { get; private set; }
        public OrderStatus Status { get; private set; }

        public decimal Total => _orderItems.Sum(i => i.SubTotal);

        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems.AsReadOnly();

        private OrderEntity() { } // For EF Core

        public OrderEntity(Guid customerId)
        {
            CustomerId = customerId;
            OrderDate = DateTime.UtcNow;
            Status = OrderStatus.Draft;
        }

        public OrderItem AddItem(Guid productId, decimal unitPrice, int quantity)
        {
            if (quantity <= 0)
                throw new InvalidOperationException("Quantity must be greater than zero.");

            var existing = _orderItems.FirstOrDefault(i => i.ProductId == productId);

            if (existing != null)
            {
                existing.AddQuantity(quantity);
                return existing;
            }

            var item = new OrderItem(productId, unitPrice, quantity);
            _orderItems.Add(item);
            return item;
        }

        public void Confirm()
        {
            if (!_orderItems.Any())
                throw new InvalidOperationException("Order must have at least one item.");

            Status = OrderStatus.Confirmed;

            AddDomainEvent(
                new OrderCreatedDomainEvent(Id, CustomerId, Total));
        }
        public void Complete()
        {
            if (Status == OrderStatus.Cancelled)
                throw new InvalidOperationException("Only confirmed orders can be completed.");            
            Status = OrderStatus.Completed;
            var items = _orderItems.Select(i => (i.ProductId, i.Quantity)).ToList();
            AddDomainEvent(new OrderCompletedDomainEvent(Id, items));
        }
        public void Cancel()
        {
            if (Status == OrderStatus.Completed)
                throw new InvalidOperationException("Completed order cannot be cancelled.");
            
            Status = OrderStatus.Cancelled;
        }
    }
}
