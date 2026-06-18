using Ecommerce.Identity.Application.Commands;
using Ecommerce.Identity.Application.Queries;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OpenIddict.Abstractions;
using OpenIddict.Server.AspNetCore;
using System.Security.Claims;
namespace Ecommerce.Identity.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllUsersQuery());
            return Ok(products);
        }

        [HttpGet("{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetUserByIdQuery(id));
            return product == null ? NotFound() : Ok(product);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand request)
        {
            var result = await _mediator.Send(request);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
        //[AllowAnonymous]
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginUserCommand request)
        //{
        //    var result = await _mediator.Send(request);

        //    return result.IsSuccess
        //        ? Ok(result.Value)
        //        : BadRequest(result.Error);
        //}

        [AllowAnonymous]
        [HttpPost("~/connect/token")]
        public async Task<IActionResult> Exchange()
        {
            var request = HttpContext.GetOpenIddictServerRequest();

            if (request.IsPasswordGrantType())
            {
                var result = await _mediator.Send(
                    new LoginUserCommand(request.Username, request.Password));

                if (!result.IsSuccess)
                    return Forbid();

                var user = result.Value;

                var claims = new List<Claim>
                {
                    new Claim(OpenIddictConstants.Claims.Subject, user.Id.ToString()),
                    new Claim(OpenIddictConstants.Claims.Email, user.Email),
                    new Claim(OpenIddictConstants.Claims.Name, user.Name)
                };

                var identity = new ClaimsIdentity(
                    claims,
                    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);
                principal.SetScopes("ecommerce_api");
                principal.SetResources("ecommerce_api");

                return SignIn(principal,
                    OpenIddictServerAspNetCoreDefaults.AuthenticationScheme);
            }

            return BadRequest();
        }

    }
}
