using CinemasService.Dtos;
using CinemasService.Models;
using CinemasService.Services.Interfaces;
using Common.Audit;
using Common.Utils;
using ScreenOps.Common;
using System.Text.Json;

namespace CinemasService.Services.Audit
{
    public class AuditableCinemaService : IAuditableCinemaService
    {
        private readonly ICinemaService _service;
        private readonly IAuditClient _auditClient;

        private readonly string _modelEntityType = typeof(Cinema).Name;

        public AuditableCinemaService(ICinemaService service, IAuditClient auditClient)
        {
            _service = service;
            _auditClient = auditClient;
        }
        public async Task<ApiResult<CinemaDto>> Create(CinemaCreateDto dto, Guid userId, AuthorInfo author)
        {
            var res = await _service.Create(dto, userId);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "CINEMA_CREATED",
                UserId = author.Id,
                EntityType = _modelEntityType,
                EntityGuid = res.Data?.Id,
                IpAddress = author.IpAddress,
                AdditionalData = DtoUtils.GetSelectedFieldsAsJson(res.Data, "Name")
            });

            return res;
        }

        public async Task<ApiResult<CinemaDto>> Update(Guid id, CinemaUpdateDto dto, AuthorInfo author)
        {
            var res = await _service.Update(id, dto);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "CINEMA_UPDATED",
                UserId = author.Id,
                EntityType = _modelEntityType,
                EntityGuid = res.Data?.Id,
                IpAddress = author.IpAddress,
                AdditionalData = JsonSerializer.Serialize(DtoUtils.GetNonNullFields(dto, "ForceUpdate"))
            });
            return res;
        }

        public async Task<ApiResult<bool>> Delete(Guid id, Guid userId, AuthorInfo author)
        {
            var res = await _service.Delete(id, userId);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "CINEMA_DELETED",
                UserId = author.Id,
                EntityType = _modelEntityType,
                EntityGuid = id,
                IpAddress = author.IpAddress,
            });
            return res;

        }

        // Not audited for now

        public Task<ApiResult<CinemaDto>> GetById(Guid id, bool includeUnpublished)
        {
            return _service.GetById(id, includeUnpublished);
        }

        public Task<ApiResult<IEnumerable<CinemaSummaryDto>>> GetAll(bool includeDeleted, bool includeUnpublished)
        {
            return _service.GetAll(includeDeleted, includeUnpublished);
        }
    }
}
