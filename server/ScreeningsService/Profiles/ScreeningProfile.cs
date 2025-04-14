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
            CreateMap<Screening, ScreeningDto>();
            CreateMap<GrpcMovieSummaryModel, MovieSummaryDto>();
            CreateMap<GrpcRoomSummaryModel, RoomSummaryDto>();

            CreateMap<ScreeningSchedule, ScreeningScheduleDto>();
            CreateMap<ScreeningScheduleTime, ScreeningTimeDto>();
        }
    }
}
