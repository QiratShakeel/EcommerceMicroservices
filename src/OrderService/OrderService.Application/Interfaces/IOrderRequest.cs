namespace Ecommerce.Orders.Application.Interfaces
{
    public interface IOrderRequest
    {
        Guid CustomerId { get; }
    }
}
