using CinemasService.Models;

namespace CinemasService.Repositories
{
    public interface ICinemaRepository
    {

        Task<IEnumerable<Cinema>> GetAll(bool includeDeleted, bool includeUnpublished);

        Task<Cinema?> GetById(Guid id, bool includeUnpublished);
        Task<Cinema?> GetByName(string name);

        Task<Cinema> Insert(Cinema cinema);

        Task SaveChanges();
    }
}
