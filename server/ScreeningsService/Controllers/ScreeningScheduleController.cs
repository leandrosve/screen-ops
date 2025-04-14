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
    [Tags("Screening Schedules")]
    [Route("schedules")]
    [Manager]
    public class ScreeningScheduleController : BaseAuthController
    {

        private readonly IScreeningScheduleService _service;
        private readonly IValidator<ScreeningScheduleSearchFiltersDto> _filtersValidator;

        public ScreeningScheduleController(IScreeningScheduleService service, IValidator<ScreeningScheduleSearchFiltersDto> filtersValidator)
        {
            _service = service;
            _filtersValidator = filtersValidator;
        }

        [HttpPost(Name = "Create Screening Schedule")]
        [ProducesResponseType(typeof(ScreeningScheduleDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(ScreeningScheduleCreateDto dto)
        {
            ApiResult<ScreeningScheduleDto> res = await _service.Create(dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet("{id}", Name = "Get Screening Schedule")]
        [ProducesResponseType(typeof(ScreeningScheduleDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResult<ScreeningScheduleDto> res = await _service.GetById(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Get Multiple Screening Schedules")]
        [ProducesResponseType(typeof(ICollection<ScreeningScheduleDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid? cinemaId, [FromQuery] Guid? roomId, [FromQuery]
        Guid? movieId, [FromQuery] ICollection<ScreeningStatusEnum>? status, [FromQuery]
        DateOnly? fromDate, [FromQuery]
        DateOnly? toDate)
        {

            var filters = new ScreeningScheduleSearchFiltersDto
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

            ApiResult<ICollection<ScreeningScheduleDto>> res = await _service.GetByFilters(filters);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}/status")]
        [ProducesResponseType(typeof(ScreeningScheduleDto), StatusCodes.Status200OK)]
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
