using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreenOps.Common;

namespace ScreeningsService.Services
{
    public interface IScreeningService
    {
        public Task<ApiResult<ScreeningDto>> Create(ScreeningCreateDto dto);

        public Task<ApiResult<ScreeningDto>> GetById(Guid id);

        public Task<ApiResult<ICollection<ScreeningDto>>> GetByFilters(ScreeningSearchFiltersDto dto);

        public Task<ApiResult<ScreeningDto>> UpdateStatus(Guid id, ScreeningStatusEnum status);

        public Task<ApiResult<bool>> Delete(Guid id);
    }
}
