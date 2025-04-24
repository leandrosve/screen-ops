using Common.Models;
using Microsoft.EntityFrameworkCore;
using MoviesService.Dtos;
using MoviesService.Models;
using ScreenOps.MoviesService.Data;
using System.Text.RegularExpressions;

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

        public async Task<PagedResult<Movie>> GetByFilters(MovieFiltersDto filters)
        {
            var query = _context.Movies
                .Include(m => m.Genres).ThenInclude(mg => mg.Genre)
                .Include(m => m.Media)
                .AsQueryable();

            if (!filters.IncludeDeleted)
            {
                query = query.Where(m => m.DeletedAt == null);
            }


            if (filters.Status != null && filters.Status.Count > 0)
            {
                query = query.Where(m => filters.Status.Contains(((int)m.Status)));
            }

            if (!String.IsNullOrEmpty(filters.SearchTerm))
            {
                // Buscar año en el search term (ej: "Avengers 2012")
                var match = Regex.Match(filters.SearchTerm, @"\b\d{4}\b");

                var searchTerm = filters.SearchTerm.ToLower();

                if (match.Success && int.TryParse(match.Value, out int year))
                {
                    query = query.Where(m => m.OriginalReleaseYear == year
                        || m.OriginalTitle.ToLower().Contains(searchTerm)
                        || m.LocalizedTitle.ToLower().Contains(searchTerm));
                }
                else
                {
                    query = query.Where(m => m.OriginalTitle.ToLower().Contains(searchTerm)
                        || m.LocalizedTitle.ToLower().Contains(searchTerm));
                }
            }

            query = query.OrderByDescending(x => x.CreatedAt);

            var totalCount = await query.CountAsync();
            var pagination = filters.Pagination;

            var offset = Math.Max(pagination.Page - 1, 0) * pagination.PageSize;
            query = query.Skip(offset).Take(pagination.PageSize);

            var res = await query.ToListAsync();

            return new PagedResult<Movie>
            {
                Items = res,
                PageNumber = pagination.Page,
                PageSize = pagination.PageSize,
                TotalCount = totalCount
            };
        }

        public async Task<Movie?> GetByExactTitleAndYearAsync(string originalTitle, int year)
        {
            return await _context.Movies
                .AsNoTracking()
                .FirstOrDefaultAsync(m =>
                    m.OriginalTitle.ToLower() == originalTitle.ToLower()
                    && m.OriginalReleaseYear == year);
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