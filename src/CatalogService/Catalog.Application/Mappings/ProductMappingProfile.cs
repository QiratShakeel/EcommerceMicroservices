using Ecommerce.Catalog.Application.Dto;
using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Application.Commands;
using AutoMapper;
using BuildingBlocks.Shared.Infrastructure;

namespace Ecommerce.Catalog.Application.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductInventory, ProductInventoryDto>()
                .ForMember(d => d.IsAvailable, o => o.MapFrom(s => s.AvailableStock > 0));
            
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.Price, o => o.MapFrom(s => s.Price.Amount))
                .ForMember(d => d.inventory, o => o.MapFrom(s => s.Inventory)); ;
            
            CreateMap<CreateProductCommand, Product>().ConstructUsing(
                cmd => new Product(
                    cmd.Name,
                    cmd.SKU,
                    new Money(cmd.Price),
                    cmd.Desc
                    )
            ).ForAllMembers(opt=>opt.Ignore());
        }
    }
}