using CinemasService.Data;
using CinemasService.Dtos;
using CinemasService.Models;
using Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace CinemasService.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private AppDBContext _context;

        public RoomRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Room>> GetAll(Guid? cinemaId)
        {
            var query = _context.Rooms.AsQueryable();

            query = query.Where(m => m.Status != EntityStatus.Deleted);

            if (cinemaId.HasValue)
            {
                query.Where(m => m.CinemaId == cinemaId);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Room>> GetByFilters(RoomSearchFiltersDto filters)
        {
            var query = _context.Rooms.AsQueryable();

            if (filters.Status == null || filters.Status.Count == 0)
            {
                query = query.Where(m => m.Status != EntityStatus.Deleted);
            }
          
            if (filters.CinemaId.HasValue)
            {
                query = query.Where(m => m.CinemaId == filters.CinemaId.Value);
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Room?> GetById(Guid id)
        {
            var query = _context.Rooms.AsQueryable();
            
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
