using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common.Controllers;
using MoviesService.Services;
using MoviesService.Dtos;

namespace ScreenOps.MoviesService.Controllers
{

    [Tags("Movies")]
    [Route("movies")]
    public class MovieController : BaseAuthController
    {
        private readonly IMovieService _service;

        public MovieController(IMovieService service) {
            _service = service; 
        }

        [HttpPost(Name = "Create Movie")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(MovieCreateDto dto)
        {
            ApiResult<MovieDto> res = await _service.Create(dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}", Name = "Update Movie")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(MovieUpdateDto dto, Guid id)
        {
            ApiResult<MovieDto> res = await _service.Update(id, dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet("{id}", Name = "Get Movie")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResult<MovieDto> res = await _service.Get(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Get All Movies")]
        [ProducesResponseType(typeof(IEnumerable<MovieDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] bool includeDeleted = false)
        {
            ApiResult<IEnumerable<MovieDto>> res = await _service.GetAll(includeDeleted);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpDelete("{id}", Name = "Delete Movie")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid userId = GetUserId();
            ApiResult<bool> res = await _service.Delete(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }
    }
}
