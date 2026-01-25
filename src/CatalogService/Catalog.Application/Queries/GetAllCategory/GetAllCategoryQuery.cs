using MediatR;
using Ecommerce.Catalog.Application.Dto;
using System.Collections.Generic;

namespace Ecommerce.Catalog.Application.Queries
{
    public class GetAllCategoryQuery : IRequest<List<CategoryDto>> { }
}
