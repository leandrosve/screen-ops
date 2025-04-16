using CinemasService.Dtos;
using Common.Audit;
using ScreenOps.Common;

namespace CinemasService.Services.Interfaces
{
    public interface IAuditableRoomService
    {
        public Task<ApiResult<RoomDto>> Create(RoomCreateDto dto, AuthorInfo info);

        public Task<ApiResult<RoomDto>> Update(Guid id, RoomUpdateDto dto, AuthorInfo info);

        public Task<ApiResult<RoomDto>> Publish(Guid id, AuthorInfo info);

        public Task<ApiResult<bool>> Delete(Guid id, AuthorInfo info);
    }
}
