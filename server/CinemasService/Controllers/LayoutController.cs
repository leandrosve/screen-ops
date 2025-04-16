using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common.Controllers;
using CinemasService.Dtos;
using CinemasService.Services.Interfaces;
using Common.Attributes;

namespace ScreenOps.CinemasService.Controllers
{

    [Tags("Layouts")]
    [Route("/layouts")]
    [Manager]
    public class LayoutController : BaseAuthController
    {
        private readonly ILayoutService _service;
        private readonly IAuditableLayoutService _auditableService;

        public LayoutController(ILayoutService service, IAuditableLayoutService auditableService)
        {
            _service = service;
            _auditableService = auditableService;
        }

        [HttpPost(Name = "Create Layout")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(LayoutCreateDto dto)
        {
            ApiResult<LayoutDto> res = await _auditableService.Create(dto, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet("{id}", Name = "Get Layout")]
        [ProducesResponseType(typeof(LayoutDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResult<LayoutDto> res = await _service.GetById(id, true);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Search Layouts")]
        [ProducesResponseType(typeof(ICollection<LayoutDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] string? name, [FromQuery] bool includeDeleted)
        {
            var filters = new LayoutSearchFiltersDto
            {
                IncludeDeleted = includeDeleted,
                Name = name
            };
            ApiResult<ICollection<LayoutDto>> res = await _service.GetByFilters(filters);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpDelete("{id}", Name = "Delete Layout")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            ApiResult<bool> res = await _auditableService.Delete(id, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

    }
}
