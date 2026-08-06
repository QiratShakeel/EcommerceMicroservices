using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using System.Threading;
using Ecommerce.Catalog.Application.Dto;
using BuildingBlocks.Shared.Exceptions;
using AutoMapper;
using Ecommerce.Catalog.Domain.Aggregates;

namespace Ecommerce.Catalog.Application.Queries
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        private readonly IProductQueries _queries;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IProductQueries queries, IMapper mapper)
        {
            _queries = queries;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken ct)
        {
            var product = await _queries.GetByIdAsync(query.Id, ct);
            if (product == null)
                throw new NotFoundException("Product not found",query.Id);            
            return _mapper.Map<ProductDto>(product);
        }
    }
}