using ScreeningsService.Dtos;
using ScreeningsService.Models;

namespace ScreeningsService.Repositories
{
    public interface IScreeningScheduleRepository
    {
        Task<ScreeningSchedule?> GetById(Guid id);

        Task<ICollection<ScreeningSchedule>> GetByFilters(ScreeningScheduleSearchFiltersDto filters);

        Task<ScreeningSchedule> Insert(ScreeningSchedule screening);

        Task SaveChanges();
    }
}
