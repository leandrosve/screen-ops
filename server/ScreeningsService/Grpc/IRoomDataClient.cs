using Contracts.Rooms;
using ScreenOps.Common;

namespace ScreeningsService.Grpc
{
     public interface IRoomDataClient
    {
        public Task<ApiResult<RoomSummaryDto?>> GetSummary(Guid id);
    }
}
