using Common.Attributes;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ScreeningsService.Dtos;
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

        public ScreeningController(IScreeningService service)
        {
            _service = service;
        }

        [HttpPost(Name = "Create Screening")]
        [ProducesResponseType(typeof(ScreeningDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(ScreeningCreateDto dto)
        {
            ApiResult<ScreeningDto> res = await _service.Create(dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }
    }
}
