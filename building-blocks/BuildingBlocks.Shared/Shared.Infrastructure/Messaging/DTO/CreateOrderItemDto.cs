using System.Text.Json.Serialization;

namespace BuildingBlocks.Shared.Infrastructure.Dto
{
    public class CreateOrderItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public CreateOrderItemDto() { }

        [JsonConstructor]
        public CreateOrderItemDto(Guid productid, int quantity)
        {
            ProductId = productid;
            Quantity = quantity;
        }
    }
}
