using CinemasService.Dtos;
using ScreenOps.Common;

namespace CinemasService.Services
{
    public interface ICinemaService
    {
        public Task<ApiResult<CinemaDto>> Create(CinemaCreateDto dto, Guid userId);

        public Task<ApiResult<CinemaDto>> Update(Guid id, CinemaUpdateDto dto);

        public Task<ApiResult<CinemaDto>> GetById(Guid id);

        public Task<ApiResult<IEnumerable<CinemaDto>>> GetAll(bool includeDeleted);

        public Task<ApiResult<bool>> Delete(Guid id, Guid userId);
    }
}
