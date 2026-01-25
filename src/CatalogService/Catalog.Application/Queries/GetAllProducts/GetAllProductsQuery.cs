using Ecommerce.Catalog.Application.Dto;
using MediatR;
using System.Collections.Generic;

namespace Ecommerce.Catalog.Application.Queries
{
    public class GetAllProductsQuery : IRequest<List<ProductDto>> { }
}
