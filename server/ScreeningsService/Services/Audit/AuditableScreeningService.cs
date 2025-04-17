using Common.Audit;
using Common.Utils;
using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreeningsService.Models;
using ScreenOps.Common;
using System.Text.Json;

namespace ScreeningsService.Services.Audit
{
    public class AuditableScreeningService : IAuditableScreeningService
    {

        private readonly IScreeningService _service;
        private readonly IAuditClient _auditClient;

        private readonly string _modelType = typeof(Screening).Name;

        public AuditableScreeningService(IScreeningService service, IAuditClient auditClient)
        {
            _service = service;
            _auditClient = auditClient;
        }

        public async Task<ApiResult<ScreeningDto>> Create(ScreeningCreateDto dto, AuthorInfo author)
        {
            var res = await _service.Create(dto);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                UserId = author.Id,
                IpAddress = author.IpAddress,
                EntityType = _modelType,
                EntityGuid = res.Data?.Id,
                Action = "SCREENING_CREATED",
                AdditionalData = JsonSerializer.Serialize(DtoUtils.GetSelectedFieldsAsJson(res.Data, "CinemaId", "MovieId", "RoomId"))
            });
            return res;
        }

        public async Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author)
        {
            var res = await _service.Delete(id);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                UserId = author.Id,
                IpAddress = author.IpAddress,
                EntityType = _modelType,
                EntityGuid = id,
                Action = "SCREENING_DELETED",
            });
            return res;
        }

        public Task<ApiResult<ICollection<ScreeningDto>>> GetByFilters(ScreeningSearchFiltersDto dto)
        {
            return _service.GetByFilters(dto);
        }

        public Task<ApiResult<ScreeningDto>> GetById(Guid id)
        {
            return _service.GetById(id);
        }

        public async Task<ApiResult<ScreeningDto>> UpdateStatus(Guid id, ScreeningStatusEnum status, AuthorInfo author)
        {
            var res = await _service.UpdateStatus(id, status);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                UserId = author.Id,
                IpAddress = author.IpAddress,
                EntityType = _modelType,
                EntityGuid = id,
                Action = "SCREENING_STATUS_UPDATED",
                AdditionalData = JsonSerializer.Serialize(new Dictionary<string, string>() { ["Status"] = status.ToString() })
            });

            return res;
        }
    }
}
