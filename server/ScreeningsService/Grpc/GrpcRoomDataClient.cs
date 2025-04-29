using AutoMapper;
using Contracts.Rooms;
using Grpc.Net.Client;
using GrpcRoomsService;
using ScreenOps.Common;

namespace ScreeningsService.Grpc
{
    public class GrpcRoomDataClient : IRoomDataClient
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        private readonly string _address;

        public GrpcRoomDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
            _address = _configuration["GrpcRooms"] ?? throw new ArgumentException("GrpcRooms URL unspecified");
        }

        public async Task<ApiResult<RoomSummaryContractDto?>> GetSummary(Guid id)
        {
            Console.WriteLine($"Calling Grpc Service {_address}");
            var channel = GrpcChannel.ForAddress(_address);
            var client = new GrpcRoom.GrpcRoomClient(channel);
            try
            {
                var res = await client.GetRoomByIdAsync(new GetRoomByIdRequest { Id = id.ToString() });

                if (res.Error != null)
                {
                    return ApiResult<RoomSummaryContractDto?>.Fail(res.Error);
                }
                return ApiResult<RoomSummaryContractDto?>.Ok(_mapper.Map<RoomSummaryContractDto>(res.Data));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not call GRPC Server {ex.Message}");
                return ApiResult<RoomSummaryContractDto?>.Fail("internal_error");
            }
        }

    }
}
