using Ecommerce.Orders.Domain.Enums;

namespace Ecommerce.Orders.Domain.ValueObjects
{
    public record Address(City city, string Street, string State);
}