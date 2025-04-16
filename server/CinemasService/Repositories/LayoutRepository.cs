using CinemasService.Data;
using CinemasService.Dtos;
using CinemasService.Models;
using Microsoft.EntityFrameworkCore;

namespace CinemasService.Repositories
{
    public class LayoutRepository : ILayoutRepository
    {
        private AppDBContext _context;

        public LayoutRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Layout>> GetAll(bool includeDeleted)
        {
            var query = _context.Layouts
               .Include(m => m.Elements)
               .AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(m => m.DeletedAt == null);
            }
            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Layout>> GetByFilters(LayoutSearchFiltersDto filters)
        {
            var query = _context.Layouts
                .Include(m => m.Elements)
                .AsQueryable();

            if (!filters.IncludeDeleted)
            {
                query = query.Where(m => m.DeletedAt == null);
            }
            if (filters.Name != null)
            {
                query.Where(x => x.Name.ToLower().Equals(filters.Name.ToLower()));
            }

            return await query.AsNoTracking().ToListAsync();
        }

        public async Task<Layout?> GetById(Guid id, bool includeDeleted)
        {
            var query = _context.Layouts
                .Include(m => m.Elements)
                .AsQueryable();
            if (!includeDeleted)
            {
                query.Where(c => c.DeletedAt == null);
            }
            return await query.Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Layout?> GetByName(string name, bool includeDeleted)
        {
            var query = _context.Layouts.AsQueryable();
            if (!includeDeleted)
            {
                query.Where(c => c.DeletedAt == null);
            }
            return await query.Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<Layout> Insert(Layout layout)
        {
            _context.Layouts.Add(layout);
            await SaveChanges();
            return layout;
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
