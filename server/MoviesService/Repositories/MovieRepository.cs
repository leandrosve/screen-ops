using Microsoft.EntityFrameworkCore;
using MoviesService.Models;
using ScreenOps.MoviesService.Data;

namespace MoviesService.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly AppDBContext _context;

        public MovieRepository(AppDBContext context)
        {
            _context = context;
        }

        public async Task<Movie?> GetById(Guid id)
        {
            return await _context.Movies
                .Include(m => m.Genres).ThenInclude(mg => mg.Genre)
                .Include(m => m.Media)
                .FirstOrDefaultAsync(m => m.Id == id && m.DeletedAt == null);
        }

        public async Task<IEnumerable<Movie>> GetAll(bool includeDeleted)
        {
            var query = _context.Movies
                .Include(m => m.Genres).ThenInclude(mg => mg.Genre)
                .Include(m => m.Media)
                .AsQueryable();

            if (!includeDeleted)
            {
                query = query.Where(m => m.DeletedAt == null);
            }

            return await query.ToListAsync();
        }

        public async Task<Movie> Insert(Movie movie)
        {
            await _context.Movies.AddAsync(movie);
            await _context.SaveChangesAsync();
            return movie;
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}