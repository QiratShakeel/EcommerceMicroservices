using AutoMapper;
using BuildingBlocks.Shared.Exceptions;
using BuildingBlocks.Shared.Results;
using Ecommerce.Identity.Application.Interfaces;
using Ecommerce.Identity.Domain.Aggregates;
using MediatR;

namespace Ecommerce.Identity.Application.Commands
{
    public class RegisterUserCommandHandler: IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public RegisterUserCommandHandler(IUserRepository userRepository, IMapper mapper, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
        }

        public async Task<Result<Guid>> Handle(RegisterUserCommand request,CancellationToken cancellationToken)
        {
            if (await _userRepository.EmailExistAsync(request.email, cancellationToken))
                return Result<Guid>.Failure("Email already exists");
            var customerRole = await _userRepository.GetRoleByNameAsync("Customer", cancellationToken);

            if (customerRole == null)
                throw new DomainException("Customer role not found");
            User.ValidatePasswordRules(request.password);
            var hashedPassword = _passwordHasher.Hash(request.password);
            var user = new User(request.name, request.email, hashedPassword);
            user.AssignRole(customerRole);
            await _userRepository.AddAsync(user,cancellationToken);
            return Result<Guid>.Success(user.Id);
        }
    }
}
