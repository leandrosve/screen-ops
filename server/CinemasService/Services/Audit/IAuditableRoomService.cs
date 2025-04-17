using CinemasService.Dtos;
using Common.Audit;
using Contracts.Rooms;
using ScreenOps.Common;

namespace CinemasService.Services.Audit
{
    public interface IAuditableRoomService
    {
        public Task<ApiResult<RoomDto>> Create(RoomCreateDto dto, AuthorInfo info);

        public Task<ApiResult<RoomDto>> Update(Guid id, RoomUpdateDto dto, AuthorInfo info);

        public Task<ApiResult<RoomDto>> Publish(Guid id, AuthorInfo info);

        public Task<ApiResult<bool>> Delete(Guid id, AuthorInfo info);

        // Not audited for now
        public Task<ApiResult<RoomDto>> GetById(Guid id, bool includeDeleted, bool includeUnpublished);
        public Task<ApiResult<RoomSummaryDto>> GetSummary(Guid id);
        public Task<ApiResult<IEnumerable<RoomDto>>> GetByFilters(RoomSearchFiltersDto dto);
    }
}
