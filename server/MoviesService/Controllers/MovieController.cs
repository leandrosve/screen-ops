using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common.Controllers;
using MoviesService.Services;
using MoviesService.Dtos;
using Common.Models;
using FluentValidation;
using Common.Attributes;
using MoviesService.Errors;
using Common.Audit;

namespace ScreenOps.MoviesService.Controllers
{

    [Tags("Movies")]
    [Route("movies")]
    [Manager]
    public class MovieController : BaseAuthController
    {
        private readonly IMovieService _service;
        private readonly IValidator<MovieFiltersDto> _validator;
        private readonly IAuditableMovieService _auditableMovieService;

        public MovieController(IMovieService service, IValidator<MovieFiltersDto> validator, IAuditableMovieService auditableMovieService)
        {
            _service = service;
            _validator = validator;
            _auditableMovieService = auditableMovieService;
        }

        [HttpPost(Name = "Create Movie")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(MovieCreateDto dto)
        {
            ApiResult<MovieDto> res = await _auditableMovieService.Create(dto, GetUserId(), GetIpAddress());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}", Name = "Update Movie")]
        [ProducesResponseType(typeof(MovieDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(MovieUpdateDto dto, Guid id)
        {
            ApiResult<MovieDto> res = await _auditableMovieService.Update(id, dto, GetUserId(), GetIpAddress());

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

        [HttpGet(Name = "Get Movies")]
        [ProducesResponseType(typeof(PagedResult<MovieDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string? searchTerm, [FromQuery] bool includeDeleted, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {

            var filters = new MovieFiltersDto
            {
                IncludeDeleted = includeDeleted,
                SearchTerm = searchTerm,
                Pagination = new(page, pageSize)
            };

            var validation = await _validator.ValidateAsync(filters);

            if (!validation.IsValid) return BadRequest(new ApiError(validation));

            ApiResult<PagedResult<MovieDto>> res = await _service.GetByFilters(filters);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpDelete("{id}", Name = "Delete Movie")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid userId = GetUserId();
            ApiResult<bool> res = await _auditableMovieService.Delete(id, GetUserId(), GetIpAddress());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }
    }
}
