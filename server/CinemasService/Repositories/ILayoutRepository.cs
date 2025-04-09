using CinemasService.Dtos;
using CinemasService.Models;

namespace CinemasService.Repositories
{
    public interface ILayoutRepository
    {

        Task<IEnumerable<Layout>> GetByFilters(LayoutSearchFiltersDto filters);

        Task<Layout?> GetById(Guid id, bool includeDeleted);

        Task<Layout> Insert(Layout layout);

        Task SaveChanges();
    }
}
