using BuildingBlocks.Shared.Results;
using Ecommerce.Identity.Application.Interfaces;
using MediatR;

namespace Ecommerce.Identity.Application.Commands
{
    public record RegisterUserCommand(string name, string email, string password): IRequest<Result<Guid>>, IUserRequest;
}
