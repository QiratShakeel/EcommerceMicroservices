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

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _mediator.Send(new GetCategoryByIdQuery(id));
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommand command)
        {
            var categoryId = await _mediator.Send(command);
            var category = await _mediator.Send(new GetCategoryByIdQuery(categoryId));
            return CreatedAtAction(actionName: nameof(GetById), controllerName: "Category", routeValues: new { id = categoryId },value: category );// value:or the category object if you want
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCategoryCommand command)
        {
            if (id != command.CategoryId) return BadRequest();
            var category = await _mediator.Send(command);
            if (category == null) return NotFound();
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, DeleteCategoryCommand command)
        {
            if (id != command.categoryId) return BadRequest();
            var category = await _mediator.Send(command);
            return Ok(category);
        }
    }
}
