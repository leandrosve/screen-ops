using CinemasService.Dtos;
using CinemasService.Models;
using Microsoft.EntityFrameworkCore;
using ScreenOps.CinemasService.Data;

namespace CinemasService.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private AppDBContext _context;

        public RoomRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAll(Guid? cinemaId, bool includeDeleted, bool includeUnpublished)
        {
            var query = _context.Rooms.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(m => m.DeletedAt == null);
            }
            if (!includeUnpublished)
            {
                query = query.Where(m => m.PublishedAt != null);
            }
            if (cinemaId.HasValue)
            {
                query.Where(m => m.CinemaId == cinemaId);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetByFilters(RoomSearchFiltersDto filters)
        {
            var query = _context.Rooms.AsQueryable();

            if (!filters.IncludeDeleted)
            {
                query = query.Where(m => m.DeletedAt == null);
            }
            if (!filters.IncludeUnpublished)
            {
                query = query.Where(m => m.PublishedAt != null);
            }
            if (filters.CinemaId.HasValue)
            {
                query = query.Where(m => m.CinemaId == filters.CinemaId.Value);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Room?> GetById(Guid id, bool includeDeleted, bool includeUnpublished)
        {
            var query = _context.Rooms.AsQueryable();
            if (!includeUnpublished)
            {
                query = query.Where(c => c.PublishedAt != null);
            }
            if (!includeDeleted)
            {
                query = query.Where(c => c.DeletedAt == null);
            }
            return await query.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Room> Insert(Room room)
        {
            _context.Rooms.Add(room);
            await SaveChanges();
            return room;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
