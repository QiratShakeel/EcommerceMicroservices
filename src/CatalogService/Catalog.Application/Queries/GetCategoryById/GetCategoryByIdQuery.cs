using MediatR;
using Ecommerce.Catalog.Application.Dto;

namespace Ecommerce.Catalog.Application.Queries
{
    public record GetCategoryByIdQuery(int Id) : IRequest<CategoryDto>;
}