using Microsoft.AspNetCore.Http;
using System.Net;

namespace Common.Extensions
{
    public static class HttpContextExtensions
    {
        public static IPAddress GetClientIpAddress(this HttpContext context)
        {
            // Obtiene la IP de conexiones detrás de proxies/load balancers
            if (context.Request.Headers.TryGetValue("X-Forwarded-For", out var forwardedFor))
            {
                return IPAddress.Parse(forwardedFor.ToString().Split(',', StringSplitOptions.RemoveEmptyEntries)[0]);
            }

            return context.Connection.RemoteIpAddress ?? IPAddress.Loopback;
        }
    }
}
