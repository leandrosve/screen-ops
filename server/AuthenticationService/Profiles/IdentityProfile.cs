using AuthenticationService.Models;
using AutoMapper;
using ScreenOps.AuthenticationService.Dtos;

namespace ScreenOps.AuthenticationService.Profiles
{
    public class IdentityProfile : Profile
    {

        public IdentityProfile() {
            CreateMap<int?, int>().ConvertUsing((src, dest) => src ?? dest);

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.JoinedAt, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()));
        }

    }
}
