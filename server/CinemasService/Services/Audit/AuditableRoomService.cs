using CinemasService.Dtos;
using CinemasService.Models;
using CinemasService.Services.Interfaces;
using Common.Audit;
using Common.Utils;
using Contracts.Rooms;
using ScreenOps.Common;

namespace CinemasService.Services.Audit
{
    public class AuditableRoomService : IAuditableRoomService
    {
        private readonly IRoomService _service;
        private readonly IAuditClient _auditClient;
        private readonly string _modelType = typeof(Room).Name;

        public AuditableRoomService(IRoomService service, IAuditClient auditClient)
        {
            _service = service;
            _auditClient = auditClient;
        }

        public async Task<ApiResult<RoomDto>> Create(RoomCreateDto dto, AuthorInfo author)
        {
            var res = await _service.Create(dto);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "ROOM_CREATED",
                UserId = author.Id,
                EntityType = _modelType,
                EntityGuid = res.Data?.Id,
                IpAddress = author.IpAddress,
                AdditionalData = DtoUtils.GetSelectedFieldsAsJson(res.Data, "Name")
            });
            return res;
        }

        public async Task<ApiResult<bool>> Delete(Guid id, AuthorInfo author)
        {
            var res = await _service.Delete(id);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "ROOM_DELETED",
                EntityType = _modelType,
                EntityGuid = id,
                UserId = author.Id,
                IpAddress = author.IpAddress,
                AdditionalData = DtoUtils.GetSelectedFieldsAsJson(res.Data, "Name")
            });
            return res;
        }
        public async Task<ApiResult<RoomDto>> Update(Guid id, RoomUpdateDto dto, AuthorInfo author)
        {
            var res = await _service.Update(id, dto);

            if (res.HasError) return res;

            await _auditClient.Log(new AuditLogDto
            {
                Action = "ROOM_UPDATED",
                EntityType = _modelType,
                EntityGuid = id,
                UserId = author.Id,
                IpAddress = author.IpAddress
            });
            return res;
        }

        public Task<ApiResult<IEnumerable<RoomDto>>> GetByFilters(RoomSearchFiltersDto dto)
        {
            return _service.GetByFilters(dto);
        }

        public Task<ApiResult<RoomDto>> GetById(Guid id)
        {
            return _service.GetById(id);
        }

        public Task<ApiResult<RoomSummaryDto>> GetSummary(Guid id)
        {
            return _service.GetSummary(id);
        }
    }
}
