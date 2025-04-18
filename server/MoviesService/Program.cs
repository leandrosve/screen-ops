﻿using Scalar.AspNetCore;
using ScreenOps.Common.Configuration;
using System.Reflection;
using Common.Configuration;
using ScreenOps.MoviesService.Data;
using MoviesService.Dtos;
using FluentValidation;
using MoviesService.Static;
using MoviesService.Repositories;
using MoviesService.Services;
using MoviesService.Errors;
using Common.Utils;
using MoviesService.Grpc;
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

services.AddScoped<IMovieRepository, MovieRepository>();
services.AddScoped<IGenreRepository, GenreRepository>();

// Services
services.AddScoped<IMovieService, MovieService>();
services.AddScoped<IAuditableMovieService, AuditableMovieService>();

//Grpc
services.AddGrpc();

// AutoMapper
services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Fluent Validation
FluentValidationConfig.Configure(builder, mvcBuilder);
services.AddValidatorsFromAssemblyContaining<MovieCreateDto>();
services.AddValidatorsFromAssemblyContaining<MovieUpdateDto>();

// JWT
AuthConfiguration.Configure(builder);

AuditClientConfiguration.Configure(builder);

// Load static data
CountryConstants.Load();
LanguageConstants.Load();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapControllers();


app.MapDefaultEndpoints();

app.UseAuthentication();
app.UseAuthorization();

// Transactions
app.UseMiddleware<TransactionMiddleware<AppDBContext>>();

// Grpc Endpoints
app.MapGrpcService<GrpcMovieService>();

app.MapGet("/status", async context =>
{
    await context.Response.WriteAsync("OK");
});

app.MapGet("/errors", () =>
{
    return Results.Ok(ErrorUtils.GetGroupedErrorConstants(typeof(MovieErrors)));
})
.WithName("Posible Errors")
.WithTags("Errors Reference");

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Servers = [
        new ScalarServer("https://localhost:7040", "Movies Service"),
        new ScalarServer("https://localhost:7000/api", "API Gateway")
    ];
});


app.Run();
