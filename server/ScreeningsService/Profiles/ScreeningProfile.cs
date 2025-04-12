using AutoMapper;
using Contracts.Movies;
using MoviesService;
using ScreeningsService.Dtos;
using ScreeningsService.Models;

namespace ScreeningsService.Profiles
{
    public class ScreeningProfile : Profile
    {
        public ScreeningProfile()
        {
            CreateMap<Screening, ScreeningDto>()
                 .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features.Select(f => f.Feature)));
            CreateMap<GrpcMovieSummaryModel, MovieSummaryDto>();
        }
    }
}
