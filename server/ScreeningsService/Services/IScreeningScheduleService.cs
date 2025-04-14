using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreenOps.Common;

namespace ScreeningsService.Services
{
    public interface IScreeningScheduleService
    {
        Task<ApiResult<ScreeningScheduleDto>> Create(ScreeningScheduleCreateDto dto);
        Task<ApiResult<ScreeningScheduleDto>> GetById(Guid id);
        Task<ApiResult<ICollection<ScreeningScheduleDto>>> GetByFilters(ScreeningScheduleSearchFiltersDto filters);
        Task<ApiResult<ScreeningScheduleDto>> UpdateStatus(Guid id, ScreeningStatusEnum status);
        Task<ApiResult<bool>> Delete(Guid id);
    }
}
