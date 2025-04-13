using Common.Models;
using MoviesService.Dtos;
using MoviesService.Models;

namespace MoviesService.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetById(Guid id);
        Task<IEnumerable<Movie>> GetAll(bool includeDeleted);

        Task<PagedResult<Movie>> GetByFilters(MovieFiltersDto filters);
        Task<Movie?> GetByExactTitleAndYearAsync(string originalTitle, int year);

        Task<Movie> Insert(Movie movie);
        Task<bool> SaveChanges();
    }
}
