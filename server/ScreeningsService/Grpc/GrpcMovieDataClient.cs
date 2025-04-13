using AutoMapper;
using Contracts.Movies;
using Grpc.Net.Client;
using MoviesService;
using ScreenOps.Common;

namespace ScreeningsService.Grpc
{
    public class GrpcMovieDataClient : IMovieDataClient
    {

        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public GrpcMovieDataClient(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public async Task<ApiResult<MovieSummaryDto?>> GetMovieSummary(Guid id)
        {
            string address = _configuration["GrpcMovies"] ?? throw new ArgumentException("GrpcMovies URL not specified");
            Console.WriteLine($"Calling Grpc Service {address}");
            var channel = GrpcChannel.ForAddress(address);
            var client = new GrpcMovie.GrpcMovieClient(channel);
            try
            {
                var res = await client.GetMovieByIdAsync(new GetMovieByIdRequest { Id = id.ToString() });

                if (res.Error != null)
                {
                    return ApiResult<MovieSummaryDto?>.Fail(res.Error);
                }
                return ApiResult<MovieSummaryDto?>.Ok(_mapper.Map<MovieSummaryDto>(res.Data));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not call GRPC Server {ex.Message}");
                return ApiResult<MovieSummaryDto?>.Fail("internal_error");
            }
        }

    }
}
