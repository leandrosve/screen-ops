using AutoMapper;
using Contracts.Movies;
using Grpc.Core;
using MoviesService.Services;
using ScreenOps.Common;

namespace MoviesService.Grpc
{
    public class GrpcMovieService : GrpcMovie.GrpcMovieBase
    {

        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public GrpcMovieService(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        public override async Task<GrpcMovieSummaryResponse> GetMovieById(GetMovieByIdRequest req, ServerCallContext context)
        {
            ApiResult<MovieSummaryDto> res = await _movieService.GetSummary(Guid.Parse(req.Id));

            if (res.HasError)
            {
                return new GrpcMovieSummaryResponse
                {
                    Data = null,
                    Error = res.Error.Error
                };
            }

            var response = new GrpcMovieSummaryResponse
            {
                Data = _mapper.Map<GrpcMovieSummaryModel>(res.Data),
                Error = null
            };

            return response;
        }
    }
}
