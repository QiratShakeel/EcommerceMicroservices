using Ecommerce.Catalog.Application.Dto;
using Ecommerce.Catalog.Application.Interfaces;
using MediatR;
using System.Threading;

namespace Ecommerce.Catalog.Application.Queries
{
    public class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        //private readonly IMapper _mapper;

        public GetCategoryByIdHandler(ICategoryRepository repository)
        {
            _repository = repository;
            //_mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery query, CancellationToken ct)
        {
            var category = await _repository.GetAsync(query.Id, ct);
            if (category == null)
                throw new KeyNotFoundException("Category not found");
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }
}