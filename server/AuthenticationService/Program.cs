using ScreenOps.AuthenticationService.Data;
using ScreenOps.AuthenticationService.Repositories;
using ScreenOps.AuthenticationService.Services;
using Scalar.AspNetCore;
using FluentValidation;
using ScreenOps.AuthenticationService.Dtos;
using ScreenOps.Common.Configuration;
using System.Reflection;
using Common.Configuration;
using Common.Utils;
using AuthenticationService.Errors;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

var services = builder.Services;

// Add controllers
var mvcBuilder = services.AddControllers();


// Add services to the container.
services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
services.AddOpenApi();

services.AddDbContext<AppDBContext>();

services.AddScoped<IUserRepository, UserRepository>();

// Services
services.AddScoped<IUserService, UserService>();
services.AddScoped<IAuthService, AuthService>();

// AutoMapper
services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Fluent Validation
FluentValidationConfig.Configure(builder, mvcBuilder);
services.AddValidatorsFromAssemblyContaining<SignUpRequestDto>();

OpenApiConfiguration.Configure(builder);
// JWT
AuthConfiguration.Configure(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapControllers();
app.MapDefaultEndpoints();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/status", async context =>
{
    await context.Response.WriteAsync("OK");
});

app.MapGet("/errors", () =>
{
    return Results.Ok(ErrorUtils.GetGroupedErrorConstants(typeof(AuthErrors), typeof(UserErrors)));
})
.WithName("Errors Reference")
.WithTags("Errors Reference");

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Servers = [
        new ScalarServer("https://localhost:7010", "Identity Service"),
        new ScalarServer("https://localhost:7000/api", "API Gateway")
    ];
});


app.Run();
