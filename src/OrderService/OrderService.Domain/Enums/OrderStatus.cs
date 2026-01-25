namespace Ecommerce.Orders.Domain.Enums
{
    public enum OrderStatus
    {
        Draft = 0,
        Pending = 1,
        Confirmed = 2,
        Paid = 3,
        Cancelled = 4,
        Completed = 5,
        //Shipped,
        //Delivered,
    }
}
