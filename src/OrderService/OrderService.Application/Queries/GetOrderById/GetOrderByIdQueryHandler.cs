using AutoMapper;
using BuildingBlocks.Shared.Exceptions;
using Ecommerce.Orders.Application.Dto;
using Ecommerce.Orders.Application.Interfaces;
using MediatR;
namespace Ecommerce.Orders.Application.Queries
{
    public class GetOrderByIdQueryHandler: IRequestHandler<GetOrderByIdQuery, OrderDto>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public GetOrderByIdQueryHandler(IOrderRepository repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<OrderDto> Handle(GetOrderByIdQuery request,CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.OrderId, cancellationToken)
                ?? throw new NotFoundException(nameof(Orders), request.OrderId);

            return _mapper.Map<OrderDto>(order);
        }
    }
}