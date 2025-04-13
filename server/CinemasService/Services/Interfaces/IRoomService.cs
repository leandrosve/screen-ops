using CinemasService.Dtos;
using Contracts.Rooms;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface IRoomService
    {
        public Task<ApiResult<RoomDto>> Create(RoomCreateDto dto);

        public Task<ApiResult<RoomDto>> Update(Guid id, RoomUpdateDto dto);

        public Task<ApiResult<RoomDto>> GetById(Guid id, bool includeDeleted, bool includeUnpublished);
        public Task<ApiResult<RoomSummaryDto>> GetSummary(Guid id);

        public Task<ApiResult<RoomDto>> Publish(Guid id);

        public Task<ApiResult<IEnumerable<RoomDto>>> GetByFilters(RoomSearchFiltersDto dto);

        public Task<ApiResult<bool>> Delete(Guid id);
    }
}
