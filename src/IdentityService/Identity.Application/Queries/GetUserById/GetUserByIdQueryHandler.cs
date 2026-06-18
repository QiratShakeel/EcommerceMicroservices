using AutoMapper;
using Ecommerce.Identity.Application.Dto;
using Ecommerce.Identity.Application.Interfaces;
using MediatR;

namespace Ecommerce.Identity.Application.Queries
{
    public class GetUserByIdQueryHandler: IRequestHandler<GetUserByIdQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByIdQuery request,CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);

            if (user is null)
                throw new KeyNotFoundException($"User with id {request.Id} not found");

            return _mapper.Map<UserDto>(user);
        }
    }
}
