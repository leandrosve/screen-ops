using Common.Audit;
using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreenOps.Common;

namespace ScreeningsService.Services
{
    public interface IAuditableScreeningScheduleService
    {
        Task<ApiResult<ScreeningScheduleDto>> Create(ScreeningScheduleCreateDto dto, AuthorInfo author);
        Task<ApiResult<ScreeningScheduleDto>> GetById(Guid id);
        Task<ApiResult<ICollection<ScreeningScheduleDto>>> GetByFilters(ScreeningScheduleSearchFiltersDto filters);
        Task<ApiResult<ScreeningScheduleDto>> UpdateStatus(Guid id, ScreeningStatusEnum status, AuthorInfo author);
        Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author);
    }
}
