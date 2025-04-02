using CinemasService.Models;
using Microsoft.EntityFrameworkCore;
using ScreenOps.CinemasService.Data;

namespace CinemasService.Repositories
{
    public class CinemaRepository : ICinemaRepository
    {
        private AppDBContext _context;

        public CinemaRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cinema>> GetAll(bool includeDeleted)
        {
            if (includeDeleted)
            {
                return await _context.Cinemas.ToListAsync();
            }

            return await _context.Cinemas.Where(c => c.DeletedAt == null).ToListAsync();
        }

        public async Task<Cinema?> GetById(Guid id)
        {
            return await _context.Cinemas.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Cinema> Insert(Cinema cinema)
        {
            _context.Cinemas.Add(cinema);
            await SaveChanges();
            return cinema;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
