using Microsoft.EntityFrameworkCore;
using MoviesService.Models;
using ScreenOps.MoviesService.Data;

namespace MoviesService.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDBContext _context;

        public GenreRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Genre?> GetById(int id)
        {
            return await _context.Genres.FindAsync(id);
        }

        public async Task<IEnumerable<Genre>> GetByIds(IEnumerable<int> ids)
        {
            return await _context.Genres
            .Where(g => ids.Contains(g.Id))
            .ToListAsync();
        }

        public async Task<ISet<int>> GetAllIds()
        {
            return await _context.Genres.Select(g => g.Id).ToHashSetAsync();
        }
        public ISet<int> GetAllIdsSync()
        {
            return _context.Genres.Select(g => g.Id).ToHashSet();
        }

    }
}