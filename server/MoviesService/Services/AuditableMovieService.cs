using Common.Audit;
using Common.Utils;
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

        public async Task<ApiResult<MovieDto>> Create(MovieCreateDto dto, Guid userId, string ipAddress)
        {
            var res = await _movieService.Create(dto);

            if (res.HasError) return res;
          
            await _auditClient.Log(new AuditLogDto
            {
                Action = "MOVIE_CREATED",
                UserId = userId,
                EntityType = "Movie",
                EntityGuid = res.Data?.Id,
                Timestamp = DateTime.UtcNow,
                IpAddress = ipAddress,
                AdditionalData = res.Data?.OriginalTitle
            });

            return res;
        }

        public async Task<ApiResult<MovieDto>> Update(Guid id, MovieUpdateDto dto, Guid userId, string ipAddress)
        {
            var res = await _movieService.Update(id, dto);

            await _auditClient.Log(new AuditLogDto
            {
                Action = "MOVIE_UPDATED",
                UserId = userId,
                EntityType = "Movie",
                EntityGuid = res.Data?.Id,
                Timestamp = DateTime.UtcNow,
                IpAddress = ipAddress,
                AdditionalData = JsonSerializer.Serialize(DtoUtils.GetNonNullFields(dto, "ForceUpdate"))
            });

            return res;
        }

        public async Task<ApiResult<bool>> Delete(Guid id, Guid userId, string ipAddress)
        {
            var res = await _movieService.Delete(id);

            await _auditClient.Log(new AuditLogDto
            {
                Action = "MOVIE_DELETED",
                UserId = userId,
                EntityType = "Movie",
                EntityGuid = id,
                Timestamp = DateTime.UtcNow,
                IpAddress = ipAddress,
            });

            return res;
        }

    }
}
