using MediatR;
using Ecommerce.Catalog.Application.Dto;

namespace Ecommerce.Catalog.Application.Queries
{
    public record GetCategoryByIdQuery(Guid Id) : IRequest<CategoryDto>;
}