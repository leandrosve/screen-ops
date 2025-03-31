using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ScreenOps.AuthenticationService.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public class BaseController : ControllerBase
    {

    }
}
