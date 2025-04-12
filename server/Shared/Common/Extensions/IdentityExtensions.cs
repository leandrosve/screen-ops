using System.Security.Claims;
using System.Security.Principal;

namespace ScreenOps.Common.Extensions
{
    public static class IdentityExtensions
    {
        public static Guid GetUserId(this IIdentity identity)
        {
            if (identity == null) return Guid.Empty;

            ClaimsIdentity? claimsIdentity = identity as ClaimsIdentity;

            if (claimsIdentity == null) return Guid.Empty;

            Claim? claim = claimsIdentity.FindFirst("id");

            if (claim == null)
                return Guid.Empty;

            return Guid.Parse(claim.Value);
        }
    }
}
