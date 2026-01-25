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
            CreateMap<Product, ProductDto>().ForMember(d => d.Price, o => o.MapFrom(s => s.Price.Amount));
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