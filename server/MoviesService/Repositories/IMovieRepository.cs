using MoviesService.Models;

namespace MoviesService.Repositories
{
    public interface IMovieRepository
    {
        Task<Movie?> GetById(Guid id);
        Task<IEnumerable<Movie>> GetAll(bool includeDeleted);
        Task<Movie> Insert(Movie movie);
        Task<bool> SaveChanges();
    }
}
