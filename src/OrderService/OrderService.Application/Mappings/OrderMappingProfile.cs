using AutoMapper;
using Ecommerce.Orders.Domain.Aggregates;
using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Application.Commands;

namespace Ecommerce.Orders.Application.Mapping
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            // Map OrderEntity -> OrderDto
            CreateMap<OrderEntity, OrderDto>()
                .ForMember(d => d.Status, o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Items, o => o.MapFrom(s => s.OrderItems))   // map collection
                .ForMember(d => d.TotalAmount, o => o.MapFrom(s => s.Total)) // computed total
                .ForMember(d => d.CreatedAt, o => o.MapFrom(s => DateTime.UtcNow)); // set current time

            // Map CreateOrderCommand -> OrderEntity
            CreateMap<CreateOrderCommandWithUser, OrderEntity>()
                .ConstructUsing(cmd => new OrderEntity(cmd.CustomerId))
                .ForAllMembers(opt => opt.Ignore());

            // Map OrderItem -> OrderItemDto
            CreateMap<OrderItem, OrderItemDto>().ForMember(x=>x.Price, o=>o.MapFrom(s=>s.UnitPrice));
        }
    }

}
