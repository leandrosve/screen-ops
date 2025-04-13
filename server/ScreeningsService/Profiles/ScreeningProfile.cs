using AutoMapper;
using Contracts.Movies;
using Contracts.Rooms;
using MoviesService;
using GrpcRoomsService;
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
            CreateMap<GrpcRoomSummaryModel, RoomSummaryDto>();
        }
    }
}
