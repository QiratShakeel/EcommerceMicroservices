using Ecommerce.Catalog.Application.Commands;
using Ecommerce.Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Ecommerce.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id:guid}", Name = "GetProductById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var product = await _mediator.Send(new GetProductByIdQuery(id));
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductCommand command)
        {
            var result = await _mediator.Send(command);
            //if(!result.IsSuccess)
            //    return BadRequest(result.Error);
            if (result.Value == Guid.Empty)
                return StatusCode(500, "Product Id was not generated");
            return CreatedAtRoute("GetProductById",new { id = result.Value }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
        {
            if (id != command.ProductId) return BadRequest();
            var product = await _mediator.Send(command);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, DeleteProductCommand command)
        {
            if (id != command.prodId) return BadRequest();
            var product = await _mediator.Send(command);
            return Ok(product);
        }
    }
}
