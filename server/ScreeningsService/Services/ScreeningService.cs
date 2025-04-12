using AutoMapper;
using Common.Services;
using Contracts.Movies;
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

        public ScreeningService(IScreeningRepository repository, IMapper mapper, IMovieDataClient movieDataClient)
        {
            _repository = repository;
            _mapper = mapper;
            _movieDataClient = movieDataClient;
        }

        public async Task<ApiResult<ScreeningDto>> Create(ScreeningCreateDto dto)
        {
            // Validate movie and room exists
            // Take duration and validate EndTime

            ApiResult<MovieSummaryDto?> movieRes = await _movieDataClient.GetMovieSummary(dto.MovieId);

            if (movieRes.HasError || movieRes.Data == null) { 
                return Fail<ScreeningDto>(movieRes.Error.Error);
            }

            MovieSummaryDto movie = movieRes.Data;

            if (dto.EndTime < dto.StartTime.AddMinutes(movie.Duration))
            {
                return Fail<ScreeningDto>(ScreeningErrors.Create.EndTimeBeforeMovieEnds);
            }

            var screening = new Screening
            {
                CinemaId = Guid.Empty,
                RoomId = Guid.Empty,
                MovieId = movie.Id,
                StartTime = dto.StartTime,
                EndTime = dto.StartTime,
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

        public Task<ApiResult<ScreeningDto>> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<ScreeningDto>> Publish(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
