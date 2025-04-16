using CinemasService.Data;
using CinemasService.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemasService.Repositories
{
    public class CinemaRepository : ICinemaRepository
    {
        private AppDBContext _context;

        public CinemaRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Cinema>> GetAll(bool includeDeleted, bool includeUnpublished)
        {
            var query = _context.Cinemas.AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(m => m.DeletedAt == null);
            }
            if (!includeUnpublished)
            {
                query = query.Where(m => m.IsPublished);
            }

            return await query.ToListAsync();
        }

        public async Task<Cinema?> GetById(Guid id, bool includeUnpublished)
        {
            var query = _context.Cinemas.AsQueryable();
            if (!includeUnpublished) {
                query.Where(c => c.IsPublished);
            }
            return await query.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Cinema?> GetByName(string name)
        {
            var query = _context.Cinemas.AsQueryable();
            return await query.Where(u => u.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
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
