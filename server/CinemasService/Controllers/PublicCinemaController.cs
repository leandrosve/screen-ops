using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common.Controllers;
using CinemasService.Services;
using CinemasService.Dtos;

namespace ScreenOps.CinemasService.Controllers
{

    [Tags("Public Cinemas")]
    [Route("public/cinemas")]
    public class PublicCinemaController : BaseController
    {
        private readonly ICinemaService _service;

        public PublicCinemaController(ICinemaService service) {
            _service = service; 
        }

        [HttpGet("{id}", Name = "Get Public Cinema")]
        [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResult<CinemaDto> res = await _service.GetById(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Get All Public Cinemas")]
        [ProducesResponseType(typeof(IEnumerable<CinemaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            ApiResult<IEnumerable<CinemaDto>> res = await _service.GetAll(false);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

    }
}
