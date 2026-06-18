using BuildingBlocks.Shared.Results;
using Ecommerce.Identity.Application.Interfaces;
using Ecommerce.Identity.Domain.Aggregates;
using MediatR;

namespace Ecommerce.Identity.Application.Commands
{
    public record LoginUserCommand(string email, string password): IRequest<Result<User>>, IUserRequest;
}
