using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common.Controllers;
using CinemasService.Dtos;
using CinemasService.Services.Interfaces;

namespace ScreenOps.CinemasService.Controllers
{

    [Tags("Public Cinemas")]
    [Route("public/cinemas")]
    public class PublicCinemaController : BaseController
    {
        private readonly IPublicCinemaService _service;

        public PublicCinemaController(IPublicCinemaService service) {
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
            ApiResult<IEnumerable<CinemaDto>> res = await _service.GetAll();

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

    }
}
