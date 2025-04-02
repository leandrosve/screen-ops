using CinemasService.Models;

namespace CinemasService.Repositories
{
    public interface ICinemaRepository
    {

        Task<IEnumerable<Cinema>> GetAll(bool includeDeleted);

        Task<Cinema?> GetById(Guid id);

        Task<Cinema> Insert(Cinema cinema);

        Task SaveChanges();
    }
}
