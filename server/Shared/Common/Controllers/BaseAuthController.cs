using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using ScreenOps.Common.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace ScreenOps.Common.Controllers
{
    [ApiController]
    [Authorize(Roles = "ADMIN, MANAGER")]
    [ProducesResponseType(typeof(ApiError), StatusCodes.Status400BadRequest)]
    public class BaseAuthController : ControllerBase
    {

        protected Guid GetUserId()
        {
            if (User.Identity == null) throw new UnauthorizedException();
            var id = User.Identity.GetUserId();
            if (id == Guid.Empty) throw new UnauthorizedException();
            return id;
        }
    }
}
