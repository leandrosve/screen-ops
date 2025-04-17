using AuditService.Dtos;
using AuditService.Services;
using Common.Attributes;
using Common.Controllers;
using Common.Models;
using Microsoft.AspNetCore.Mvc;
using ScreenOps.Common;

namespace AuditService.Controllers
{
    [Tags("Audit Logs")]
    [Route("audit-logs")]
    [Admin]
    public class AuditController : BaseAuthController
    {

        private readonly IAuditLogService _service;

        public AuditController(IAuditLogService service)
        {
            _service = service;
        }

        [HttpGet("{id}", Name = "Get Audit Log")]
        [ProducesResponseType(typeof(AuditLogDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            ApiResult<AuditLogDto> res = await _service.GetById(id);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }

        [HttpGet(Name = "Get Multiple Audit Logs")]
        [ProducesResponseType(typeof(PagedResult<AuditLogDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(
            [FromQuery] Guid? userId,
            [FromQuery] string? action,
            [FromQuery] string? entityType,
            [FromQuery] Guid? entityGuid,
            [FromQuery] int? entityId,
            [FromQuery] DateTime? fromDate,
            [FromQuery] DateTime? toDate,
            [FromQuery] string? ipAddress,
            [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {

            var filters = new AuditLogSearchFiltersDto
            {
                UserId = userId,
                Action = action,
                EntityType = entityType,
                EntityGuid = entityGuid,
                EntityId = entityId,
                FromDate = fromDate,
                ToDate = toDate,
                IpAddress = ipAddress,
                Pagination = new(page, pageSize)
            };

            ApiResult<PagedResult<AuditLogDto>> res = await _service.GetByFilters(filters);

            if (res.HasError) return BadRequest(res.Error);

            return Ok(res.Data);
        }
    }
}
