using CinemasService.Dtos;
using Common.Audit;
using ScreenOps.Common;

namespace CinemasService.Services.Audit
{
    public interface IAuditableLayoutService
    {
        public Task<ApiResult<LayoutDto>> Create(LayoutCreateDto dto, AuthorInfo author);

        public Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author);

        public Task<ApiResult<LayoutDto>> GetById(Guid id, bool includeDeleted);

        public Task<ApiResult<ICollection<LayoutDto>>> GetByFilters(LayoutSearchFiltersDto filters);
    }
}
