using AutoMapper;
using ScreenOps.AuthenticationService.Dtos;
using ScreenOps.AuthenticationService.Models;

namespace ScreenOps.AuthenticationService.Profiles
{
    public class IdentityProfile : Profile
    {

        public IdentityProfile() {

            CreateMap<User, UserDto>()
                .ForMember(dest => dest.JoinedAt, opt => opt.MapFrom(src => src.CreatedAt));

        }

    }
}
