using CinemasService.Dtos;
using Common.Audit;
using ScreenOps.Common;

namespace CinemasService.Services.Audit
{
    public interface IAuditableCinemaService
    {
         Task<ApiResult<CinemaDto>> Create(CinemaCreateDto dto, Guid userId, AuthorInfo author);

         Task<ApiResult<CinemaDto>> Update(Guid id, CinemaUpdateDto dto, AuthorInfo author);

         Task<ApiResult<bool>> Delete(Guid id, Guid userId, AuthorInfo author);

        Task<ApiResult<CinemaDto>> GetById(Guid id, bool includeUnpublished) ;

        Task<ApiResult<IEnumerable<CinemaDto>>> GetAll(bool includeDeleted, bool includeUnpublished);
    }
}
