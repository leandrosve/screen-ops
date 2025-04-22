using ScreenOps.Errors;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace ScreenOps.Common.Configuration
{
    public static class FluentValidationConfig
    {

        private static bool _enableCustomErrorDetail = true;
        public static void Configure(WebApplicationBuilder builder, IMvcBuilder mvcBuilder)
        {
            var services = builder.Services;
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            ValidatorOptions.Global.PropertyNameResolver = (a, b, c) => b?.Name?.ToLowerInvariant();

            if (_enableCustomErrorDetail)
            {
                mvcBuilder.ConfigureApiBehaviorOptions(options =>
                {
                    options.InvalidModelStateResponseFactory = ValidationErrorDetail.MakeValidationResponse;
                });
            }
        }
    }
}
