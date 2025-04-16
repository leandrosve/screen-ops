using CinemasService.Dtos;
using Common.Audit;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface IAuditableCinemaService
    {
         Task<ApiResult<CinemaDto>> Create(CinemaCreateDto dto, Guid userId, AuthorInfo author);

         Task<ApiResult<CinemaDto>> Update(Guid id, CinemaUpdateDto dto, AuthorInfo author);

         Task<ApiResult<bool>> Delete(Guid id, Guid userId, AuthorInfo author);
    }
}
