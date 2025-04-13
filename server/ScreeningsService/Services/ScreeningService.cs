using AutoMapper;
using Common.Services;
using Contracts.Movies;
using Contracts.Rooms;
using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreeningsService.Errors;
using ScreeningsService.Grpc;
using ScreeningsService.Models;
using ScreeningsService.Repositories;
using ScreenOps.Common;

namespace ScreeningsService.Services
{
    public class ScreeningService : BaseService, IScreeningService
    {
        private readonly IScreeningRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMovieDataClient _movieDataClient;
        private readonly IRoomDataClient _roomDataClient;


        public ScreeningService(IScreeningRepository repository, IMapper mapper, IMovieDataClient movieDataClient, IRoomDataClient roomDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _movieDataClient = movieDataClient;
            _roomDataClient = roomDataClient;
        }

        public async Task<ApiResult<ScreeningDto>> Create(ScreeningCreateDto dto)
        {

            var isRoomAvailable = await _repository.CheckRoomAvailability(dto.RoomId, dto.Date, dto.StartTime, dto.EndTime);

            if (!isRoomAvailable)
            {
                return Fail<ScreeningDto>(ScreeningErrors.Create.RoomOccupied);
            }

            ApiResult<MovieSummaryDto?> movieRes = await _movieDataClient.GetMovieSummary(dto.MovieId);

            if (movieRes.HasError || movieRes.Data == null) { 
                return Fail<ScreeningDto>(movieRes.Error.Error);
            }

            MovieSummaryDto movie = movieRes.Data;

            if (dto.EndTime < dto.StartTime.AddMinutes(movie.Duration))
            {
                return Fail<ScreeningDto>(ScreeningErrors.Create.EndTimeBeforeMovieEnds);
            }

            ApiResult<RoomSummaryDto?> roomRes = await _roomDataClient.GetSummary(dto.RoomId);

            if (roomRes.HasError || roomRes.Data == null)
            {
                return Fail<ScreeningDto>(roomRes.Error.Error);
            }
            RoomSummaryDto room = roomRes.Data;

            var screening = new Screening
            {
                CinemaId = room.CinemaId,
                RoomId = room.Id,
                MovieId = movie.Id,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                Status = ScreeningStatusEnum.Draft
            };

            var features = dto.Features.ToHashSet().Select((f) => ( new ScreeningFeature { 
                Feature = f,
                Screening = screening
            }));

            screening.Features = features.ToList();

            await _repository.Insert(screening);

            return Ok(_mapper.Map<ScreeningDto>(screening));
        }

        public Task<ApiResult<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<ICollection<ScreeningDto>>> GetByFilters(ScreeningSearchFiltersDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<ScreeningDto>> GetById(Guid id)
        {
            var screening = await _repository.GetById(id);

            if (screening == null)
            {
                return Fail<ScreeningDto>(ScreeningErrors.Get.ScreeningNotFound);
            }

            return Ok(_mapper.Map<ScreeningDto>(screening));
        }

        public async Task<ApiResult<ScreeningDto>> UpdateStatus(Guid id, ScreeningStatusEnum status)
        {
            var screening = await _repository.GetById(id);

            if (screening == null)
            {
                return Fail<ScreeningDto>(ScreeningErrors.UpdateStatus.ScreeningNotFound);
            }

            if (new[] { ScreeningStatusEnum.Published, ScreeningStatusEnum.Active }.Contains(status))
            {
                if (screening.Date < DateOnly.FromDateTime(DateTime.UtcNow))
                {
                    return Fail<ScreeningDto>(ScreeningErrors.Get.ScreeningNotFound);
                }
            }

            screening.Status = status;

            await _repository.SaveChanges();
            return Ok(_mapper.Map<ScreeningDto>(screening));
        }
    }
}
