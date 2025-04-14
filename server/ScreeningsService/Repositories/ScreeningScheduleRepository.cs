using Microsoft.EntityFrameworkCore;
using ScreeningsService.Data;
using ScreeningsService.Dtos;
using ScreeningsService.Models;

namespace ScreeningsService.Repositories
{
    public class ScreeningScheduleRepository : IScreeningScheduleRepository
    {
        private AppDBContext _context;

        public ScreeningScheduleRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<ICollection<ScreeningSchedule>> GetByFilters(ScreeningScheduleSearchFiltersDto filters)
        {
            var query = _context.ScreeningSchedules.Include(s => s.Screenings).Include(s => s.Times).AsQueryable();

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

            if (filters.FromDate.HasValue || filters.ToDate.HasValue)
            {
                var fromDate = filters.FromDate ?? DateOnly.MinValue;
                var toDate = filters.ToDate ?? DateOnly.MaxValue;

                query = query.Where(m =>
                    // Cualquier solapamiento con el rango de fechas
                    (m.StartDate <= toDate && m.EndDate >= fromDate)
                );
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<ScreeningSchedule?> GetById(Guid id)
        {
            var query = _context.ScreeningSchedules.Include(x => x.Screenings).Include(x => x.Times).AsQueryable();

            return await query.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ScreeningSchedule> Insert(ScreeningSchedule screeningSchedule)
        {
            _context.ScreeningSchedules.Add(screeningSchedule);
            await SaveChanges();
            return screeningSchedule;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
