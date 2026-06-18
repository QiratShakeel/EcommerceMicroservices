using Ecommerce.Identity.Application.Dto;
using MediatR;

namespace Ecommerce.Identity.Application.Queries
{
    public record GetAllUsersQuery : IRequest<List<UserDto>>; 
}
