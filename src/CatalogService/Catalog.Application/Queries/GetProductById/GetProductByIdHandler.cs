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
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IProductRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetProductByIdQuery query, CancellationToken ct)
        {
            var product = await _repository.GetByIdAsync(query.Id, ct);
            if (product == null)
                throw new NotFoundException("Product not found",query.Id);            
            return _mapper.Map<ProductDto>(product);
        }
    }
}