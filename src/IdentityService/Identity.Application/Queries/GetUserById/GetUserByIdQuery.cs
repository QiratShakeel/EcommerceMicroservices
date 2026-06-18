using Ecommerce.Identity.Application.Dto;
using MediatR;

namespace Ecommerce.Identity.Application.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
}
