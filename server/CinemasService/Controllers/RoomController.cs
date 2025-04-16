using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common.Controllers;
using CinemasService.Dtos;
using CinemasService.Services.Interfaces;
using Common.Attributes;

namespace ScreenOps.CinemasService.Controllers
{

    [Tags("Rooms")]
    [Route("/rooms")]
    [Manager]
    public class RoomController : BaseAuthController
    {
        private readonly IRoomService _service;
        private readonly IAuditableRoomService _auditableService;


        public RoomController(IRoomService service, IAuditableRoomService auditableService)
        {
            _service = service;
            _auditableService = auditableService;
        }

        [HttpPost(Name = "Create Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(RoomCreateDto dto)
        {
            ApiResult<RoomDto> res = await _auditableService.Create(dto, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}", Name = "Update Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(RoomUpdateDto dto, Guid id)
        {
            ApiResult<RoomDto> res = await _auditableService.Update(id, dto, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet("{id}", Name = "Get Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResult<RoomDto> res = await _service.GetById(id, true, true);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Get All Rooms")]
        [ProducesResponseType(typeof(IEnumerable<RoomDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid? cinemaId, [FromQuery] bool includeDeleted, [FromQuery] bool includeUnpublished)
        {
            var filters = new RoomSearchFiltersDto
            {
                CinemaId = cinemaId,
                IncludeDeleted = includeDeleted,
                IncludeUnpublished = includeUnpublished
            };
            ApiResult<IEnumerable<RoomDto>> res = await _service.GetByFilters(filters);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpDelete("{id}", Name = "Delete Room")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            ApiResult<bool> res = await _auditableService.Delete(id, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }


        [HttpPost("{id}/publish", Name = "Publish Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Publish(Guid id)
        {
            ApiResult<RoomDto> res = await _auditableService.Publish(id, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

    }
}
