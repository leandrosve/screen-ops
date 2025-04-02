using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ScreenOps.Common.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public class BaseController : ControllerBase
    {

    }
}
