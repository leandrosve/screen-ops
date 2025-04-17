using Common.Audit;
using Common.Models;
using Contracts.Movies;
using MoviesService.Dtos;
using ScreenOps.Common;

namespace MoviesService.Services
{
    public interface IAuditableMovieService
    {
        Task<ApiResult<MovieDto>> Create(MovieCreateDto dto, AuthorInfo author);

        Task<ApiResult<MovieDto>> Update(Guid id, MovieUpdateDto dto, AuthorInfo author);

        Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author);

        Task<ApiResult<MovieDto>> Get(Guid id);

        Task<ApiResult<MovieSummaryDto>> GetSummary(Guid id);

        Task<ApiResult<PagedResult<MovieDto>>> GetByFilters(MovieFiltersDto filters);
    }
}
