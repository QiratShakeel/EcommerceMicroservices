using Ecommerce.Orders.Application.Commands;
using Ecommerce.Orders.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ecommerce.Orders.API.Extensions;
namespace Ecommerce.Orders.API.Controllers
{
    [Authorize]
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
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            var userId = User.GetUserId();
            var command = new CreateOrderCommandWithUser
            (
                userId,
                request.Items
            );
            var result = await _mediator.Send(command);

            return result.IsSuccess
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await _mediator.Send(new GetOrderByIdQuery(id)));
    }
}