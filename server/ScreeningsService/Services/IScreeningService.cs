using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreenOps.Common;

namespace ScreeningsService.Services
{
    public interface IScreeningService
    {
        Task<ApiResult<ScreeningDto>> Create(ScreeningCreateDto dto);

        Task<ApiResult<ScreeningDto>> GetById(Guid id);

        Task<ApiResult<ICollection<ScreeningDto>>> GetByFilters(ScreeningSearchFiltersDto dto);

        Task<ApiResult<ScreeningDto>> UpdateStatus(Guid id, ScreeningStatusEnum status);

        Task<ApiResult<bool>> Delete(Guid id);
    }
}
