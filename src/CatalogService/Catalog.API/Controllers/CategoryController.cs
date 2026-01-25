using Microsoft.AspNetCore.Mvc;
using MediatR;
using Ecommerce.Catalog.Application.Commands;
using Ecommerce.Catalog.Application.Queries;
using Ecommerce.Catalog.Application.Commands.CreateCategory;
namespace Ecommerce.Catalog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var category = await _mediator.Send(new GetAllCategoryQuery());
            return Ok(category);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = categoryId });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateCategoryCommand command)
        {
            if (id != command.CategoryId) return BadRequest();
            var category = await _mediator.Send(command);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, DeleteCategoryCommand command)
        {
            if (id != command.categoryId) return BadRequest();
            var category = await _mediator.Send(command);
            return Ok(category);
        }
    }
}
