﻿using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using CinemasService.Dtos;
using Common.Attributes;
using CinemasService.Services.Audit;
using Common.Controllers;

namespace CinemasService.Controllers
{

    [Tags("Rooms")]
    [Route("/rooms")]
    [Manager]
    public class RoomController : BaseAuthController
    {
        private readonly IAuditableRoomService _service;

        public RoomController(IAuditableRoomService auditableService)
        {
            _service = auditableService;
        }

        [HttpPost(Name = "Create Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(RoomCreateDto dto)
        {
            ApiResult<RoomDto> res = await _service.Create(dto, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}", Name = "Update Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(RoomUpdateDto dto, Guid id)
        {
            ApiResult<RoomDto> res = await _service.Update(id, dto, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet("{id}", Name = "Get Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(Guid id)
        {
            ApiResult<RoomDto> res = await _service.GetById(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Get All Rooms")]
        [ProducesResponseType(typeof(IEnumerable<RoomSummaryDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] Guid? cinemaId, [FromQuery] bool includeDeleted, [FromQuery] ICollection<int>? status)
        {
            var filters = new RoomSearchFiltersDto
            {
                CinemaId = cinemaId,
                Status = status,
            };
            ApiResult<IEnumerable<RoomSummaryDto>> res = await _service.GetByFilters(filters);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpDelete("{id}", Name = "Delete Room")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            ApiResult<bool> res = await _service.Delete(id, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

    }
}
