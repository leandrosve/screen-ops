﻿using Microsoft.AspNetCore.Mvc;
using CinemasService.Dtos;
using Common.Attributes;
using ScreenOps.Common;
using CinemasService.Services.Audit;
using Common.Controllers;

namespace CinemasService.Controllers
{

    [Tags("Cinemas")]
    [Route("cinemas")]
    [Manager]
    public class CinemaController : BaseAuthController
    {
        private readonly IAuditableCinemaService _service;

        public CinemaController( IAuditableCinemaService service )
        {
            _service = service;
        }

        [HttpPost(Name = "Create Cinema")]
        [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CinemaCreateDto dto)
        {
            Guid userId = GetUserId();
            ApiResult<CinemaDto> res = await _service.Create(dto, userId, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpPatch("{id}", Name = "Update Cinema")]
        [ProducesResponseType(typeof(CinemaDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(CinemaUpdateDto dto, Guid id)
        {
            ApiResult<CinemaDto> res = await _service.Update(id, dto, GetAuthorInfo());

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
            ApiResult<IEnumerable<CinemaSummaryDto>> res = await _service.GetAll(deleted, unpublished);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpDelete("{id}", Name = "Delete Cinema")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            Guid userId = GetUserId();
            ApiResult<bool> res = await _service.Delete(id, userId, GetAuthorInfo());

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }
    }
}
