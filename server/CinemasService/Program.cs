using Scalar.AspNetCore;
using ScreenOps.Common.Configuration;
using System.Reflection;
using ScreenOps.CinemasService.Data;
using CinemasService.Repositories;
using CinemasService.Services;
using CinemasService.Dtos;
using FluentValidation;
using Common.Configuration;
using CinemasService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Common.Middleware;

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

services.AddScoped<ICinemaRepository, CinemaRepository>();
services.AddScoped<IRoomRepository, RoomRepository>();
services.AddScoped<ILayoutRepository, LayoutRepository>();

// Services
services.AddScoped<ICinemaService, CinemaService>();
services.AddScoped<IPublicCinemaService, PublicCinemaService>();
services.AddScoped<IRoomService, RoomService>();
services.AddScoped<ILayoutService, LayoutService>();

// AutoMapper
services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Fluent Validation
FluentValidationConfig.Configure(builder, mvcBuilder);
services.AddValidatorsFromAssemblyContaining<CinemaCreateDto>();
services.AddValidatorsFromAssemblyContaining<CinemaUpdateDto>();
services.AddValidatorsFromAssemblyContaining<RoomCreateDto>();
services.AddValidatorsFromAssemblyContaining<RoomUpdateDto>();

// JWT
AuthConfiguration.Configure(builder);

var app = builder.Build();

// Transactions
app.UseMiddleware<TransactionMiddleware<AppDBContext>>();

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

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Servers = [
        new ScalarServer("https://localhost:7030", "Cinemas Service"),
        new ScalarServer("https://localhost:7000/api", "API Gateway")
    ];
});


app.Run();
