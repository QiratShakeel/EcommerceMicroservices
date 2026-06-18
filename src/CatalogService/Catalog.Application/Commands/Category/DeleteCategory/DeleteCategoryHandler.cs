using Ecommerce.Catalog.Domain.Aggregates;
using Ecommerce.Catalog.Domain.ValueObjects;
using Ecommerce.Catalog.Domain.Enums;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using System.Threading;

namespace Ecommerce.Catalog.Application.Commands
{
    public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
    {
        private readonly ICategoryRepository _repository;
        public DeleteCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
            //_mapper = mapper;
        }
        public async Task<bool> Handle(DeleteCategoryCommand cmd, CancellationToken ct)
        {
            var category = await _repository.GetAsync(cmd.categoryId, ct);
            if (category == null)
                return false;
            if (await _repository.HasChildrenAsync(cmd.categoryId,ct))
                throw new InvalidOperationException(
                    "Cannot delete category having child categories.");

            await _repository.DeleteAsync(category);
            return true;
        }
    }
}