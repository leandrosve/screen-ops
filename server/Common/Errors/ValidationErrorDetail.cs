using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;

namespace ScreenOps.Errors
{
    public class ValidationErrorDetail
    {

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

             var result = new ApiError(errorMessage, context.HttpContext.TraceIdentifier);

             var response = new BadRequestObjectResult(result);

             response.ContentTypes.Add("application/problem+json");

             return response;
        }

    }
}