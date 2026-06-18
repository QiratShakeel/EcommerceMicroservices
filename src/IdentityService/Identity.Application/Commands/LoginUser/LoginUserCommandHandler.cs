using BuildingBlocks.Shared.Results;
using Ecommerce.Identity.Application.Interfaces;
using Ecommerce.Identity.Domain.Aggregates;
using MediatR;
using System.Security.Authentication;

namespace Ecommerce.Identity.Application.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<User>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, IJwtTokenGenerator jwtTokenGenerator )
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        public async Task<Result<User>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(request.email, cancellationToken);
            if (user == null) return Result<User>.Failure("User Not Found");
            if (!_passwordHasher.Verify(request.password, user.PasswordHash))
                return Result<User>.Failure("Invalid Credentials");
            //var token = _jwtTokenGenerator.GenerateToken(user);
            user.LogLogin();
            return Result<User>.Success(user);    
        }
    }
}