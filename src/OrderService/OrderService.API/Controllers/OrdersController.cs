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
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand request)
        {
            // Controller sirf Command create karke Mediator ko deta hai
            //var command = new CreateOrderCommand(request.CustomerId, request.Items);

            var result = await _mediator.Send(request);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
    }
}