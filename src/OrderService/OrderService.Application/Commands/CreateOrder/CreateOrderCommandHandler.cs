using Ecommerce.Orders.Application.Interfaces;
using Ecommerce.Orders.Domain.Aggregates;
using BuildingBlocks.Shared.Results;
using MediatR;
using Ecommerce.Orders.Application.Commands;
using AutoMapper;
namespace Ecommerce.Orders.Application.Commands
{
    public class CreateOrderCommandHandler: IRequestHandler<CreateOrderCommand, Result<Guid>>
    {
        private readonly IOrderRepository _repository;
        private readonly IMapper _mapper;

        public CreateOrderCommandHandler(IOrderRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateOrderCommand request,CancellationToken cancellationToken)
        {
            var order = _mapper.Map<OrderEntity>(request);

            foreach (var item in request.Items)
            {
                order.AddItem(item.ProductId, item.Price, item.Quantity);
            }

            order.Confirm();

            await _repository.AddAsync(order);

            return Result<Guid>.Success(order.Id);
        }
    }
}