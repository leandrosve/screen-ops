using Common.Attributes;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreeningsService.Services;
using ScreenOps.Common;
using ScreenOps.Common.Controllers;

namespace ScreeningsService.Controllers
{
    [Tags("Screenings")]
    [Route("screenings")]
    [Manager]
    public class ScreeningController : BaseAuthController
    {

        private readonly IScreeningService _service;
        private readonly IValidator<ScreeningSearchFiltersDto> _filtersValidator;

        public ScreeningController(IScreeningService service, IValidator<ScreeningSearchFiltersDto> filtersValidator)
        {
            _service = service;
            _filtersValidator = filtersValidator;
        }

        [HttpPost(Name = "Create Screening")]
        [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(ScreeningCreateDto dto)
        {
            ApiResult<ScreeningDto> res = await _service.Create(dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Get Multiple Screenings")]
        [ProducesResponseType(typeof(ICollection<ScreeningDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid? cinemaId, [FromQuery] Guid? roomId, [FromQuery]
        Guid? movieId, [FromQuery] ICollection<ScreeningStatusEnum>? status, [FromQuery]
        DateOnly? fromDate, [FromQuery]
        DateOnly? toDate)
        {

            var filters = new ScreeningSearchFiltersDto
            {
                CinemaId = cinemaId,
                RoomId = roomId,
                MovieId = movieId,
                Status = status,
                FromDate = fromDate,
                ToDate = toDate
            };

            var validation = await _filtersValidator.ValidateAsync(filters);

            if (!validation.IsValid) return BadRequest(new ApiError(validation));

            ApiResult<ICollection<ScreeningDto>> res = await _service.GetByFilters(filters);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet("{id}", Name = "Get Screening")]
        [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResult<ScreeningDto> res = await _service.GetById(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}/status", Name = "Update Screening Status")]
        [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateScreeningStatus(
         [FromRoute] Guid id,
         [FromBody] ScreeningUpdateStatusDto request)
        {
            var res = await _service.UpdateStatus(id, request.Status);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }
    }
}
