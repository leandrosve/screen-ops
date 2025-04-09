using CinemasService.Dtos;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface ILayoutService
    {
        public Task<ApiResult<LayoutDto>> Create(LayoutCreateDto dto);

        public Task<ApiResult<LayoutDto>> GetById(Guid id, bool includeDeleted);

        public Task<ApiResult<ICollection<LayoutDto>>> GetByFilters(LayoutSearchFiltersDto filters);

        public Task<ApiResult<bool>> Delete(Guid id);
    }
}
