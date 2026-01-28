using Ecommerce.Catalog.Domain.Entities;
using BuildingBlocks.Shared.Infrastructure;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using System.Threading;

namespace Ecommerce.Catalog.Application.Commands.CreateCategory
{
    public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _repository;
        //private readonly IMapper _mapper;
        public CreateCategoryHandler(ICategoryRepository repository)
        {
            _repository = repository;
            //_mapper = mapper;
        }
        public async Task<Guid> Handle(CreateCategoryCommand cmd, CancellationToken ct)
        {
            var category = new Category(cmd.Name, cmd.Desc, cmd.ParentId);
            await _repository.AddAsync(category, ct);
            return category.Id;
        }
    }
}