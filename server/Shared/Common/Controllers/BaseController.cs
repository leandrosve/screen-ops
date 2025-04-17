using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ScreenOps.Common;

namespace Common.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public class BaseController : ControllerBase
    {

    }
}
