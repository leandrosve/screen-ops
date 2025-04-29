using CinemasService.Dtos;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface ICinemaService
    {
        public Task<ApiResult<CinemaDto>> Create(CinemaCreateDto dto, Guid userId);

        public Task<ApiResult<CinemaDto>> Update(Guid id, CinemaUpdateDto dto);

        public Task<ApiResult<CinemaDto>> GetById(Guid id, bool includeUnpublished);

        public Task<ApiResult<IEnumerable<CinemaSummaryDto>>> GetAll(bool includeDeleted, bool includeUnpublished);

        public Task<ApiResult<bool>> Delete(Guid id, Guid userId);
    }
}
