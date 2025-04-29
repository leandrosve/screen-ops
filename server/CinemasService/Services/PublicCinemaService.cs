using CinemasService.Dtos;
using CinemasService.Services.Interfaces;
using ScreenOps.Common;

namespace CinemasService.Services
{
    public class PublicCinemaService : IPublicCinemaService
    {
        private readonly ICinemaService _cinemaService;

        public PublicCinemaService(ICinemaService cinemaService)
        {
            _cinemaService = cinemaService;
        }

        public Task<ApiResult<IEnumerable<CinemaSummaryDto>>> GetAll()
        {
           return _cinemaService.GetAll(false, false);
        }

        public Task<ApiResult<CinemaDto>> GetById(Guid id)
        {
            return _cinemaService.GetById(id, false);
        }
    }
}
