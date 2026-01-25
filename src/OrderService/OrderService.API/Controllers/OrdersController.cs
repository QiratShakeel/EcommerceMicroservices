using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ecommerce.Orders.Application.Commands;
using Ecommerce.Orders.Application.Queries;
namespace Ecommerce.Orders.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderCommand command)
            => Ok(await _mediator.Send(command));

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
    }
}