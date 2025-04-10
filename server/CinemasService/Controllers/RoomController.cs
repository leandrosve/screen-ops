﻿using ScreenOps.Common;
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

        public RoomController(IRoomService service)
        {
            _service = service;
        }

        [HttpPost(Name = "Create Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(RoomCreateDto dto)
        {
            ApiResult<RoomDto> res = await _service.Create(dto);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}", Name = "Update Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(RoomUpdateDto dto, Guid id)
        {
            ApiResult<RoomDto> res = await _service.Update(id, dto);

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
            ApiResult<bool> res = await _service.Delete(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }


        [HttpPost("{id}/publish", Name = "Publish Room")]
        [ProducesResponseType(typeof(RoomDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Publish(Guid id)
        {
            ApiResult<RoomDto> res = await _service.Publish(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

    }
}
