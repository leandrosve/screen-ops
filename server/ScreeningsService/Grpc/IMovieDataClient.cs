using Contracts.Movies;
using ScreenOps.Common;

namespace ScreeningsService.Grpc
{
     public interface IMovieDataClient
    {
        public Task<ApiResult<MovieSummaryDto?>> GetMovieSummary(Guid id);
    }
}
