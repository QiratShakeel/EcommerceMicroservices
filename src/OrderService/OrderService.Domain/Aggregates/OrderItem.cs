using BuildingBlocks.Shared.Infrastructure;
namespace Ecommerce.Orders.Domain.Aggregates
{
    public class OrderItem : Entity
    {
        public Guid ProductId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public decimal SubTotal => UnitPrice * Quantity;

        private OrderItem() { } // EF

        public OrderItem(Guid productId, decimal unitPrice, int quantity)
        {
            ProductId = productId;
            //ProductName = productName;
            //Sku = sku; // snapshot from catalog
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public void AddQuantity(int qty)
        {
            Quantity += qty;
        }
    }

}