using AutoMapper;
using Ecommerce.Catalog.Application.Dto;
using Ecommerce.Catalog.Application.Interfaces;
using Ecommerce.Catalog.Domain.Aggregates;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Ecommerce.Catalog.Application.Queries
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<ProductDto>>
    {
        private readonly IProductQueries _queries;
        //private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(IProductQueries queries)
        {
            _queries = queries;
            //_mapper = mapper;
        }

        public async Task<List<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _queries.GetAllAsync(cancellationToken);
            return products.ToList();
            //return _mapper.Map<ProductDto>(products);
            //return products.Select(p => new ProductDto
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Description = p.Description,
            //    Price = p.Price.Amount,
            //    SKU = p.SKU,
            //    inventory = new ProductInventoryDto{AvailableStock = p.Inventory.AvailableStock, IsAvailable = p.Inventory.AvailableStock > 0 } 
            //}).ToList();
        }
    }
}