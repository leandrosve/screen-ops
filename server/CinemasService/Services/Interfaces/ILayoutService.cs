using CinemasService.Dtos;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface ILayoutService
    {
        public Task<ApiResult<LayoutDto>> Create(LayoutCreateDto dto);

        public Task<ApiResult<LayoutDto>> Update(Guid id, LayoutUpdateDto dto);

        public Task<ApiResult<LayoutDto>> GetById(Guid id, bool includeDeleted);

        public Task<ApiResult<ICollection<LayoutSummaryDto>>> GetByFilters(LayoutSearchFiltersDto filters);

        public Task<ApiResult<bool>> Delete(Guid id);
    }
}
