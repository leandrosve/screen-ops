using Microsoft.EntityFrameworkCore;
using ScreeningsService.Data;
using ScreeningsService.Dtos;
using ScreeningsService.Models;

namespace ScreeningsService.Repositories
{
    public class ScreeningRepository : IScreeningRepository
    {
        private AppDBContext _context;

        public ScreeningRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Screening>> GetByFilters(ScreeningSearchFiltersDto filters)
        {
            var query = _context.Screenings.AsQueryable();

            if (filters.CinemaId.HasValue)
            {
                query = query.Where(m => m.CinemaId == filters.CinemaId.Value);
            }
            if (filters.RoomId.HasValue)
            {
                query = query.Where(m => m.RoomId == filters.RoomId.Value);
            }
            if (filters.MovieId.HasValue)
            {
                query = query.Where(m => m.MovieId == filters.MovieId.Value);
            }
            if (filters.Status != null && filters.Status.Count > 0)
            {
                query = query.Where(m => filters.Status.Contains(m.Status));
            }
            if (filters.Features != null && filters.Features.Count > 0)
            {
                query = query.Where(m => m.Features.Any(f => filters.Features.Contains(f.Feature)));
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<bool> CheckRoomAvailability(Guid roomId, DateOnly date, TimeOnly startTime, TimeOnly endTime)
        {
            var query = _context.Screenings.AsQueryable()
                .Where(s => s.RoomId == roomId && s.Date == date
                && (s.StartTime.IsBetween(startTime, endTime) || s.EndTime.IsBetween(startTime, endTime)));

            var occupied = await query.AsNoTracking().AnyAsync();
            return !occupied;
        }

        public async Task<Screening?> GetById(Guid id)
        {
            var query = _context.Screenings.AsQueryable();

            return await query.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Screening> Insert(Screening screening)
        {
            _context.Screenings.Add(screening);
            await SaveChanges();
            return screening;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
