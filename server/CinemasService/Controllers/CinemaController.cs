using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common.Controllers;
using CinemasService.Dtos;
using CinemasService.Services.Interfaces;

namespace ScreenOps.CinemasService.Controllers
{

    [Tags("Cinemas")]
    [Route("cinemas")]
    public class CinemaController : BaseAuthController
    {
        private readonly ICinemaService _service;

        public CinemaController(ICinemaService service) {
            _service = service; 
        }

        [HttpPost(Name = "Create Cinema")]
        [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CinemaCreateDto dto)
        {
            Guid userId = GetUserId();
            ApiResult<CinemaDto> res = await _service.Create(dto, userId);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}", Name = "Update Cinema")]
        [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(CinemaUpdateDto dto, Guid id)
        {
            ApiResult<CinemaDto> res = await _service.Update(id, dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet("{id}", Name = "Get Cinema")]
        [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResult<CinemaDto> res = await _service.GetById(id, true);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Get All Cinemas")]
        [ProducesResponseType(typeof(IEnumerable<CinemaDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] bool deleted = false, [FromQuery] bool unpublished = true)
        {
            ApiResult<IEnumerable<CinemaDto>> res = await _service.GetAll(deleted, unpublished);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpDelete("{id}", Name = "Delete Cinema")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid userId = GetUserId();
            ApiResult<bool> res = await _service.Delete(id, userId);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }
    }
}
