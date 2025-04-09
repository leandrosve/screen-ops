using CinemasService.Dtos;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface IPublicCinemaService
    {
        public Task<ApiResult<CinemaDto>> GetById(Guid id);
        public Task<ApiResult<IEnumerable<CinemaDto>>> GetAll();
    }
}
