using Ecommerce.Orders.Domain.Enums;

namespace Ecommerce.Orders.Application.Dto
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedAt = DateTime.UtcNow;
    }
}