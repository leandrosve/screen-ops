using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScreenOps.Common
{
    public class ApiResult<T>
    {
        [JsonIgnore]
        public bool Success { get; set; }

        public ApiError Error { get; set; } = null!;

         [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        [JsonIgnore]
        public bool HasError => Error != null;

        public static ApiResult<T> Ok(T data) => new() { Success = true, Data = data };

        public static ApiResult<T> Ok(T data, bool asd)
        {
            return new ApiResult<T> { Success = true, Data = data };
        }

        public static ApiResult<T> Fail(string error)
        {
           return new ApiResult<T>
            {
                Success = false,
                Error = new ApiError(error)
            };
        }

        public static ApiResult<T> Fail(ApiError error)
        {
            return new ApiResult<T>
            {
                Success = false,
                Error = error
            };
        }

        public static ApiResult<T> Fail(string error, string traceId)
        {
            return new ApiResult<T>
            {
                Success = false,
                Error = new ApiError(error, traceId)
            };
        }
    }
}
