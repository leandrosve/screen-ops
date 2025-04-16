using CinemasService.Dtos;
using Common.Audit;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface IAuditableLayoutService
    {
        public Task<ApiResult<LayoutDto>> Create(LayoutCreateDto dto, AuthorInfo author);

        public Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author);
    }
}
