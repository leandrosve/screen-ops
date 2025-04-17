using Common.Audit;
using Common.Utils;
using ScreeningsService.Dtos;
using ScreeningsService.Enums;
using ScreeningsService.Models;
using ScreenOps.Common;
using System.Text.Json;

namespace ScreeningsService.Services.Audit
{
    public class AuditableScreeningScheduleService : IAuditableScreeningScheduleService
    {
        private readonly IScreeningScheduleService _service;
        private readonly IAuditClient _auditClient;

        private readonly string _entityType = typeof(ScreeningSchedule).Name;
        public AuditableScreeningScheduleService(IScreeningScheduleService service, IAuditClient auditClient)
        {
            _auditClient = auditClient;
            _service = service;
        }

        public async Task<ApiResult<ScreeningScheduleDto>> Create(ScreeningScheduleCreateDto dto, AuthorInfo author)
        {
            var res = await _service.Create(dto);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                UserId = author.Id,
                IpAddress = author.IpAddress,
                EntityType = _entityType,
                EntityGuid = res.Data?.Id,
                Action = "SCREENING_SCHEDULE_CREATED",
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
                EntityType = _entityType,
                EntityGuid = id,
                Action = "SCREENING_SCHEDULE_DELETED",
                AdditionalData = JsonSerializer.Serialize(DtoUtils.GetSelectedFieldsAsJson(res.Data, "CinemaId", "MovieId", "RoomId"))
            });
            return res;
        }

        public Task<ApiResult<ICollection<ScreeningScheduleDto>>> GetByFilters(ScreeningScheduleSearchFiltersDto filters)
        {
            return _service.GetByFilters(filters);
        }

        public Task<ApiResult<ScreeningScheduleDto>> GetById(Guid id)
        {
            return _service.GetById(id);
        }

        public async Task<ApiResult<ScreeningScheduleDto>> UpdateStatus(Guid id, ScreeningStatusEnum status, AuthorInfo author)
        {
            var res = await _service.UpdateStatus(id, status);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                UserId = author.Id,
                IpAddress = author.IpAddress,
                EntityType = _entityType,
                EntityGuid = id,
                Action = "SCREENING_SCHEDULE_STATUS_UPDATED",
                AdditionalData = JsonSerializer.Serialize(new Dictionary<string, string>() { ["Status"] = status.ToString() })
            });
            return res;
        }
    }
}
