using AutoMapper;
using CinemasService.Services.Interfaces;
using Contracts.Rooms;
using Grpc.Core;
using GrpcRoomsService;
using ScreenOps.Common;

namespace CinemasService.Grpc
{
    public class GrpcRoomService : GrpcRoom.GrpcRoomBase
    {

        private readonly IRoomService _roomService;
        private readonly IMapper _mapper;

        public GrpcRoomService(IRoomService roomService, IMapper mapper)
        {
            _roomService = roomService;
            _mapper = mapper;
        }

        public override async Task<GrpcRoomSummaryResponse> GetRoomById(GetRoomByIdRequest req, ServerCallContext context)
        {
            ApiResult<RoomSummaryContractDto> res = await _roomService.GetSummary(Guid.Parse(req.Id));

            if (res.HasError)
            {
                return new GrpcRoomSummaryResponse
                {
                    Data = null,
                    Error = res.Error.Error
                };
            }

            var response = new GrpcRoomSummaryResponse
            {
                Data = _mapper.Map<GrpcRoomSummaryModel>(res.Data),
                Error = null
            };

            return response;
        }
    }
}
