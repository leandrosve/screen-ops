using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;

namespace ScreenOps.Errors
{
    public class ValidationErrorDetail
    {

        private static readonly string GUID_ERROR_PREFIX = "The JSON value could not be converted to System.Guid";

        private static readonly string DATE_ERROR_PREFIX = "The JSON value could not be converted to System.DateOnly";
        public static IActionResult MakeValidationResponse(ActionContext context)
        {

            var errorMessage = "internal_validation_error";

           
            foreach (var keyModelStatePair in context.ModelState)
            {
                var errors = keyModelStatePair.Value.Errors;


                if (errors.Count > 0 && errors.Last() != null)
                {
                    errorMessage = errors.Last().ErrorMessage;
                   
                }
            }

            if (errorMessage.StartsWith(GUID_ERROR_PREFIX))
            {
                errorMessage = "invalid_guid_format";
            }
            if (errorMessage.StartsWith(DATE_ERROR_PREFIX))
            {
                errorMessage = "invalid_date_format";
            }

             var result = new ApiError(errorMessage, context.HttpContext.TraceIdentifier);

             var response = new BadRequestObjectResult(result);

             response.ContentTypes.Add("application/problem+json");

             return response;
        }

    }
}