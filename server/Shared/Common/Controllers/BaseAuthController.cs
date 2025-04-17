using ScreenOps.Common;
using Microsoft.AspNetCore.Mvc;
using Common.Exceptions;
using Microsoft.AspNetCore.Http;
using ScreenOps.Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Common.Extensions;
using Common.Audit;

namespace Common.Controllers
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

        protected string GetIpAddress()
        {
            var clientIp = HttpContext.GetClientIpAddress();
            return clientIp.ToString();
        }

        protected AuthorInfo GetAuthorInfo()
        {
            return new AuthorInfo { Id = GetUserId(), IpAddress = GetIpAddress() };
        }
    }
}
