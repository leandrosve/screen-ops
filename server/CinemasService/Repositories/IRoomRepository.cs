using CinemasService.Dtos;
using CinemasService.Models;

namespace CinemasService.Repositories
{
    public interface IRoomRepository
    {

        Task<IEnumerable<Room>> GetAll(Guid? cinemaId);

        Task<IEnumerable<Room>> GetByFilters(RoomSearchFiltersDto filters);

        Task<Room?> GetById(Guid id);

        Task<Room> Insert(Room cinema);

        Task SaveChanges();
    }
}
