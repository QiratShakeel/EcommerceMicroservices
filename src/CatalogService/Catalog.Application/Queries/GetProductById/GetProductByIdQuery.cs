using MediatR;
using Ecommerce.Catalog.Application.Dto;

namespace Ecommerce.Catalog.Application.Queries
{
    public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
}