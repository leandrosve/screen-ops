using AutoMapper;
using CinemasService.Dtos;
using CinemasService.Models;

namespace CinemasService.Profiles
{
    public class CinemaProfile : Profile
    {

        public CinemaProfile() {

            CreateMap<Cinema, CinemaDto>();

        }

    }
}
