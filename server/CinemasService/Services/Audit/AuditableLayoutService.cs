using CinemasService.Dtos;
using CinemasService.Models;
using CinemasService.Services.Interfaces;
using Common.Audit;
using Common.Utils;
using ScreenOps.Common;
using System.Text.Json;

namespace CinemasService.Services.Audit
{
    public class AuditableLayoutService: IAuditableLayoutService
    {

        private readonly ILayoutService _service;
        private readonly IAuditClient _auditClient;

        private readonly string _modelType = typeof(Layout).Name;

        public AuditableLayoutService(ILayoutService service, IAuditClient auditClient)
        {
            _service = service;
            _auditClient = auditClient;
        }

        public async Task<ApiResult<LayoutDto>> Create(LayoutCreateDto dto, AuthorInfo author)
        {
            var res = await _service.Create(dto);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "LAYOUT_CREATED",
                UserId = author.Id,
                EntityType = _modelType,
                EntityGuid = res.Data?.Id,
                IpAddress = author.IpAddress,
                AdditionalData = DtoUtils.GetSelectedFieldsAsJson(res.Data, "Name")
            });

            return res;
        }
        public async Task<ApiResult<LayoutDto>> Update(Guid id, LayoutUpdateDto dto, AuthorInfo author)
        {
            var res = await _service.Update(id, dto);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "LAYOUT_UPDATED",
                UserId = author.Id,
                EntityType = _modelType,
                EntityGuid = res.Data?.Id,
                IpAddress = author.IpAddress,
                AdditionalData = JsonSerializer.Serialize(DtoUtils.GetNonNullFields(dto, "Elements"))
            });
            return res;
        }

        public async Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author)
        {
            var res = await _service.Delete(id);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "LAYOUT_DELETED",
                UserId = author.Id,
                EntityType = _modelType,
                EntityGuid = id,
                IpAddress = author.IpAddress,
            });

            return res;
        }

        public Task<ApiResult<ICollection<LayoutSummaryDto>>> GetByFilters(LayoutSearchFiltersDto filters)
        {
            return _service.GetByFilters(filters);
        }

        public Task<ApiResult<LayoutDto>> GetById(Guid id, bool includeDeleted)
        {
            return _service.GetById(id, includeDeleted);
        }
    }
}
