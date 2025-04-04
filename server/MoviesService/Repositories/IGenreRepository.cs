using MoviesService.Models;

namespace MoviesService.Repositories
{
    public interface IGenreRepository
    {
        Task<Genre?> GetById(int id);
        Task<IEnumerable<Genre>> GetByIds(IEnumerable<int> ids);
        Task<ISet<int>> GetAllIds();
        ISet<int> GetAllIdsSync();


    }
}
