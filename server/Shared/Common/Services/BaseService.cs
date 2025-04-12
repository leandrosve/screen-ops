using ScreenOps.Common;

namespace Common.Services
{
    public class BaseService
    {

        public ApiResult<T> Ok<T>(T data)
        {
            return ApiResult<T>.Ok(data);
        }

        public ApiResult<T> Fail<T>(string error)
        {
            return ApiResult<T>.Fail(error);
        }

    }
}
