using Ecommerce.Catalog.Domain.Aggregates;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using System.Threading;

namespace Ecommerce.Catalog.Application.Commands
{
    public class UpdatebCategoryHandler : IRequestHandler<UpdateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _repository;
        public UpdatebCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
            //_mapper = mapper;
        }
        public async Task<Guid> Handle(UpdateCategoryCommand cmd, CancellationToken ct)
        {
            var category = await _repository.GetAsync(cmd.CategoryId, ct);
            if (category == null)
                throw new InvalidOperationException("Category Not Found");
            category.UpdateCategory(cmd.Name, cmd.Desc, cmd.ParentId);
            await _repository.UpdateAsync(category, ct);
            return category.Id;
        }
    }
}