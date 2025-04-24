using Common.Audit;
using Common.Models;
using Common.Utils;
using Contracts.Movies;
using MoviesService.Dtos;
using ScreenOps.Common;
using System.Text.Json;

namespace MoviesService.Services
{
    public class AuditableMovieService: IAuditableMovieService
    {
        private readonly IMovieService _movieService;
        private readonly IAuditClient _auditClient;

        public AuditableMovieService(IMovieService movieService, IAuditClient auditClient)
        {
            _movieService = movieService;
            _auditClient = auditClient;
        }

        public async Task<ApiResult<MovieDto>> Create(MovieCreateDto dto, AuthorInfo author)
        {
            var res = await _movieService.Create(dto);

            if (res.HasError) return res;
          
            await _auditClient.Log(new AuditLogDto
            {
                Action = "MOVIE_CREATED",
                UserId = author.Id,
                EntityType = "Movie",
                EntityGuid = res.Data?.Id,
                Timestamp = DateTime.UtcNow,
                IpAddress = author.IpAddress,
                AdditionalData = res.Data?.OriginalTitle
            });

            return res;
        }

        public async Task<ApiResult<MovieDto>> Update(Guid id, MovieUpdateDto dto, AuthorInfo author)
        {
            var res = await _movieService.Update(id, dto);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "MOVIE_UPDATED",
                UserId = author.Id,
                EntityType = "Movie",
                EntityGuid = res.Data?.Id,
                Timestamp = DateTime.UtcNow,
                IpAddress = author.IpAddress,
                AdditionalData = JsonSerializer.Serialize(DtoUtils.GetNonNullFields(dto, "ForceUpdate"))
            });

            return res;
        }

        public async Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author)
        {
            var res = await _movieService.Delete(id);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "MOVIE_DELETED",
                UserId = author.Id,
                EntityType = "Movie",
                EntityGuid = id,
                Timestamp = DateTime.UtcNow,
                IpAddress = author.IpAddress,
            });

            return res;
        }

        public Task<ApiResult<MovieDto>> Get(Guid id)
        {
            return _movieService.Get(id);
        }

        public Task<ApiResult<MovieSummaryDto>> GetSummary(Guid id)
        {
            return _movieService.GetSummary(id);
        }

        public Task<ApiResult<PagedResult<MovieDto>>> GetByFilters(MovieFiltersDto filters)
        {
            return _movieService.GetByFilters(filters);
        }
    }
}
