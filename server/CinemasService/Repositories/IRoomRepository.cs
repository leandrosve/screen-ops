using CinemasService.Dtos;
using CinemasService.Models;

namespace CinemasService.Repositories
{
    public interface IRoomRepository
    {

        Task<IEnumerable<Room>> GetAll(Guid? cinemaId, bool includeDeleted, bool includeUnpublished);

        Task<IEnumerable<Room>> GetByFilters(RoomSearchFiltersDto filters);

        Task<Room?> GetById(Guid id, bool includeDeleted, bool includeUnpublished);

        Task<Room> Insert(Room cinema);

        Task SaveChanges();
    }
}
