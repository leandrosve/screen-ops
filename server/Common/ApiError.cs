using FluentValidation.Results;
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

        public ApiError(ValidationResult error)
        {
            if (error.IsValid || error.Errors.Count <= 0) {
                Error = "internal_error";
            }
            Error = error.Errors[0].ErrorMessage;
        }
    }
}
