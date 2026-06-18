using AutoMapper;
using Ecommerce.Identity.Application.Commands;
using Ecommerce.Identity.Application.Dto;
using Ecommerce.Identity.Domain.Aggregates;

namespace Ecommerce.Identity.Application.Mappings
{
    public class UserMappingProfile: Profile 
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>().ForMember(dest => dest.Roles,
                       opt => opt.MapFrom(src => src.Roles.Select(r => r.Name)));

            CreateMap<RegisterUserCommand, User>().ConstructUsing(x => new User(x.name, x.email, x.password));
        }
    }
}