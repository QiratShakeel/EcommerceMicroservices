using AutoMapper;
using Ecommerce.Identity.Application.Dto;
using Ecommerce.Identity.Application.Interfaces;
using Ecommerce.Identity.Domain.Aggregates;
using MediatR;

namespace Ecommerce.Identity.Application.Queries
{
    public record GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllUsersQueryHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync(cancellationToken);
            if (users is null)
                throw new KeyNotFoundException($"Users not found");
            return _mapper.Map<List<UserDto>>(users);
        }
    }
}
