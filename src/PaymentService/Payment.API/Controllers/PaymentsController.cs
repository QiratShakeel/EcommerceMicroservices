using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ecommerce.Payment.Application.Commands;
//using Ecommerce.Payment.Application.Queries;
namespace Ecommerce.Payment.API.Controllers
{
    [ApiController]
    [Route("api/payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Process(ProcessPaymentCommand command)
            => Ok(await _mediator.Send(command));
    }
}