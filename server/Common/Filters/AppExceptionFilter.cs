using Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using ScreenOps.Common;

namespace ScreenOps.Filters
{
    public class AppExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            ApiError error = new ApiError("internal_error");
            error.TraceId = context.HttpContext.TraceIdentifier;
            if (typeof(ControllerException).IsAssignableFrom(context.Exception.GetType()))
            {
                ControllerException exception = (ControllerException) context.Exception;
                error.Error = exception.GetMessage();
                context.Result = new JsonResult(error) { StatusCode = exception.GetHttpCode() };
                return;
            }
            error.Error = context.Exception.Message;
            context.Result = new JsonResult(error) { StatusCode = 500 };
            return;
        }
    }
}