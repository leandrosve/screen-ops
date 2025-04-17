using Common.Audit;
using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreenOps.Common;

namespace ScreeningsService.Services.Audit
{
    public interface IAuditableScreeningService
    {
        Task<ApiResult<ScreeningDto>> Create(ScreeningCreateDto dto, AuthorInfo author);

        Task<ApiResult<ScreeningDto>> GetById(Guid id);

        Task<ApiResult<ICollection<ScreeningDto>>> GetByFilters(ScreeningSearchFiltersDto dto);

        Task<ApiResult<ScreeningDto>> UpdateStatus(Guid id, ScreeningStatusEnum status, AuthorInfo author);

        Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author);
    }
}
