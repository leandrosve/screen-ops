using MoviesService.Dtos;
using ScreenOps.Common;

namespace MoviesService.Services
{
    public interface IMovieService
    {
        Task<ApiResult<MovieDto>> Create(MovieCreateDto dto);

        Task<ApiResult<MovieDto>> Update(Guid id, MovieUpdateDto dto);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<MovieDto>> Get(Guid id);

        Task<ApiResult<IEnumerable<MovieDto>>> GetAll(bool includeDeleted);
    }
}
