using ApiGateway;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("ocelot.json")
                            .Build();
var services = builder.Services;


services.AddOcelot(configuration)
        .AddSingletonDefinedAggregator<AllErrorsAggregator>();

var corsPolicy = "_allowScalarUI";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy =>
    {
        policy.WithOrigins("https://localhost:7010")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();
app.UseCors(corsPolicy);
await app.UseOcelot();

app.Run();
