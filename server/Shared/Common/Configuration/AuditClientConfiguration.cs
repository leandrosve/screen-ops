using Common.Audit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Configuration
{
    public static class AuditClientConfiguration
    {
        public static void Configure(WebApplicationBuilder builder)
        {
            builder.Services.Configure<AuditClientConfig>(builder.Configuration.GetSection("AuditClient"));

            builder.Services.AddSingleton<IAuditClient, AuditClient>();

            builder.Services.AddHostedService<AuditClientInitializerService>();
        }
    }
}
