using MoviesService.Dtos;
using ScreenOps.Common;

namespace MoviesService.Services
{
    public interface IAuditableMovieService
    {
        Task<ApiResult<MovieDto>> Create(MovieCreateDto dto, Guid userId, string ipAddress);

        Task<ApiResult<MovieDto>> Update(Guid id, MovieUpdateDto dto, Guid userId, string ipAddress);

        Task<ApiResult<bool>> Delete(Guid id, Guid userId, string ipAddress);
    }
}
