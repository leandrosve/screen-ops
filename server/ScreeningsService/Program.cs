using Scalar.AspNetCore;
using ScreenOps.Common.Configuration;
using System.Reflection;
using Common.Configuration;
using ScreeningsService.Data;
using ScreeningsService.Services;
using ScreeningsService.Repositories;
using Common.Utils;
using ScreeningsService.Errors;
using ScreeningsService.Dtos;
using FluentValidation;
using ScreeningsService.Enums;
using ScreeningsService.Grpc;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

var services = builder.Services;

// Add controllers
var mvcBuilder = services.AddControllers();


// Add services to the container.
services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//services.AddOpenApi();
OpenApiConfiguration.Configure(builder);


services.AddDbContext<AppDBContext>();

services.AddScoped<IScreeningRepository, ScreeningRepository>();

// Services
services.AddScoped<IScreeningService, ScreeningService>();

// Grpc Data client
services.AddScoped<IMovieDataClient, GrpcMovieDataClient>();

// AutoMapper
services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Fluent Validation
FluentValidationConfig.Configure(builder, mvcBuilder);
services.AddValidatorsFromAssemblyContaining<ScreeningCreateDto>();

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
    return Results.Ok(ErrorUtils.GetGroupedErrorConstants(typeof(ScreeningErrors)));
})
.WithName("Posible Errors")
.WithTags("Errors Reference");


app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Servers = [
        new ScalarServer("https://localhost:7050", "Screening Service"),
        new ScalarServer("https://localhost:7000/api", "API Gateway")
    ];
});


app.Run();
