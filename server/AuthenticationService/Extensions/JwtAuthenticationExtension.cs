using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ScreenOps.AuthenticationService.Extensions
{
    public static class JwtAuthenticationExtension
    {

        public static void AddJwtAuthentication(this IServiceCollection services) { 
            
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.SaveToken = true;
                //var key = config.GetValue<string>("JwtSettings:Key") ?? throw new ArgumentNullException("Jwt Token Signing key was not provided");
                var key = "asdasd";
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                };
            });
        }
    }
}
