namespace Ecommerce.Orders.Application.Dto
{
    public class OrderItemDto
    {
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
