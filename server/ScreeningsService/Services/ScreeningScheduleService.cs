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
    public class ScreeningScheduleService : BaseService, IScreeningScheduleService
    {
        private readonly IScreeningRepository _screeningRepository;
        private readonly IScreeningScheduleRepository _repository;

        private readonly IMapper _mapper;
        private readonly IMovieDataClient _movieDataClient;
        private readonly IRoomDataClient _roomDataClient;


        public ScreeningScheduleService(IScreeningRepository screeningRepository, IScreeningScheduleRepository repository, IMapper mapper, IMovieDataClient movieDataClient, IRoomDataClient roomDataClient)
        {
            _screeningRepository = screeningRepository;
            _repository = repository;
            _mapper = mapper;
            _movieDataClient = movieDataClient;
            _roomDataClient = roomDataClient;
        }

        public async Task<ApiResult<ScreeningScheduleDto>> Create(ScreeningScheduleCreateDto dto)
        {

            if (HasOverlappingTimes(dto.Times))
            {
                return Fail<ScreeningScheduleDto>(ScreeningScheduleErrors.Create.TimesOverlap);
            }

            // Validar que la pelicula y la sala existen

            ApiResult<MovieSummaryDto?> movieRes = await _movieDataClient.GetMovieSummary(dto.MovieId);

            if (movieRes.Data == null) return Fail<ScreeningScheduleDto>(movieRes.Error.Error);

            MovieSummaryDto movie = movieRes.Data;

            var movieDuration = movie.Duration;
            foreach (var screeningTime in dto.Times)
            {
                if (screeningTime.End < screeningTime.Start.AddMinutes(movieDuration))
                {
                    return Fail<ScreeningScheduleDto>(ScreeningScheduleErrors.Create.EndTimeBeforeMovieEnds);
                }
            }

            ApiResult<RoomSummaryDto?> roomRes = await _roomDataClient.GetSummary(dto.RoomId);

            if (roomRes.Data == null) return Fail<ScreeningScheduleDto>(roomRes.Error.Error);

            RoomSummaryDto room = roomRes.Data;

            var features = dto.Features.ToHashSet().ToList();

            // Create Schedule
            var schedule = new ScreeningSchedule
            {
                CinemaId = room.CinemaId,
                MovieId = movie.Id,
                RoomId = room.Id,
                Features = features,
                DaysOfWeek = dto.DaysOfWeek,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };

            var screeningTimes = dto.Times
                .Select(t => new ScreeningScheduleTime
                {
                    Start = t.Start,
                    End = t.End,
                    Schedule = schedule
                }).ToList();

            schedule.Times = screeningTimes;

            // Calcular las fechas y tiempos individuales para cada screening
            List<(DateOnly, ScreeningTimeDto)> individualTimes = GetIndividualScreeningTimes(dto);

            List<Screening> screenings = new List<Screening>();

            foreach (var (date, screeningTime) in individualTimes)
            {
                // Verificar disponibilidad
                var isAvailable = await _screeningRepository.CheckRoomAvailability(dto.RoomId, date, screeningTime.Start, screeningTime.End);

                if (!isAvailable)
                {
                    var error = ScreeningScheduleErrors.Create.RoomOccupied
                        .Replace("{date}", date.ToString())
                        .Replace("{start_time}", screeningTime.Start.ToTimeSpan().ToString())
                        .Replace("{end_time}", screeningTime.End.ToTimeSpan().ToString());

                    return Fail<ScreeningScheduleDto>(error);
                }

                screenings.Add(new Screening
                {
                    CinemaId = room.CinemaId,
                    Date = date,
                    EndTime = screeningTime.End,
                    StartTime = screeningTime.Start,
                    MovieId = dto.MovieId,
                    RoomId = dto.RoomId,
                    Features = features,
                    ScreeningSchedule = schedule,
                    Status = schedule.Status
                });
            }

            await _repository.Insert(schedule);

            await _screeningRepository.InsertMultiple(screenings);

            return Ok(_mapper.Map<ScreeningScheduleDto>(schedule));
        }


        public Task<ApiResult<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResult<ScreeningScheduleDto>> UpdateStatus(Guid id, ScreeningStatusEnum status)
        {
            var schedule = await _repository.GetById(id);
            if (schedule == null)
            {
                return Fail<ScreeningScheduleDto>(ScreeningScheduleErrors.Get.ScreeningScheduleNotFound);
            }

            schedule.Status = status;

            schedule.Screenings.ForEach(s => s.Status = status);

            await _repository.SaveChanges();
            await _screeningRepository.SaveChanges();

            return Ok(_mapper.Map<ScreeningScheduleDto>(schedule));
        }

        public async Task<ApiResult<ScreeningScheduleDto>> GetById(Guid id)
        {
            var schedule = await _repository.GetById(id);
            if (schedule == null)
            {
                return Fail<ScreeningScheduleDto>(ScreeningScheduleErrors.Get.ScreeningScheduleNotFound);
            }
            return Ok(_mapper.Map<ScreeningScheduleDto>(schedule));
        }

        public async Task<ApiResult<ICollection<ScreeningScheduleDto>>> GetByFilters(ScreeningScheduleSearchFiltersDto filters)
        {
            var schedule = await _repository.GetByFilters(filters);
           
            return Ok(_mapper.Map<ICollection<ScreeningScheduleDto>>(schedule));
        }

        private List<(DateOnly, ScreeningTimeDto)> GetIndividualScreeningTimes(ScreeningScheduleCreateDto dto)
        {

            var startDate = dto.StartDate;
            var endDate = dto.EndDate;
            var validDates = new HashSet<DateOnly>();

            var individual = new List<(DateOnly, ScreeningTimeDto)>();

            for (var date = startDate; date <= endDate; date = date.AddDays(1))
            {
                if (dto.DaysOfWeek.Contains(date.DayOfWeek))
                {
                    validDates.Add(date);
                }
            }

            foreach (var date in validDates)
            {
                foreach (var screeningTime in dto.Times)
                {
                    individual.Add((date, screeningTime));
                }
            }
            return individual;
        }

        private bool HasOverlappingTimes(ICollection<ScreeningTimeDto> times)
        {
            // Ordenar los tiempos por hora de inicio para facilitar la comparación
            var orderedTimes = times.OrderBy(t => t.Start).ToList();

            for (int i = 0; i < orderedTimes.Count - 1; i++)
            {
                var current = orderedTimes[i];
                var next = orderedTimes[i + 1];

                if (current.Start > current.End && next.Start > next.End)
                {
                    return true;
                }
            }

            return false;
        }

    }
}
