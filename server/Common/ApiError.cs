using System.Text.Json.Serialization;

namespace ScreenOps.Common
{
    public class ApiError
    {
        public string Error { get; set; } = "internal_error";

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string TraceId { get; set; } = "";

        public ApiError(string error, string traceId)
        {
            Error = error;
            TraceId = traceId;
        }

        public ApiError(string error)
        {
            Error = error;
        }
    }
}
