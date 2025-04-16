using AuditService.Events;
using AuditService.Repositories;
using AuditService.Services;
using Common.Configuration;
using Common.Middleware;
using FluentValidation;
using Scalar.AspNetCore;
using ScreenOps.CinemasService.Data;
using ScreenOps.Common.Configuration;
using System.Reflection;

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

services.AddScoped<IAuditLogRepository, AuditLogRepository>();

// Services
services.AddScoped<IAuditLogService, AuditLogService>();

services.AddSingleton<IEventProcessor, EventProcessor>();

services.AddHostedService<MessageBusSubscriber>();

// AutoMapper
services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Fluent Validation
FluentValidationConfig.Configure(builder, mvcBuilder);
//services.AddValidatorsFromAssemblyContaining<ScreeningCreateDto>();

// JWT
AuthConfiguration.Configure(builder);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapControllers();
app.MapDefaultEndpoints();

app.UseAuthentication();
app.UseAuthorization();

// Transactions
app.UseMiddleware<TransactionMiddleware<AppDBContext>>();

app.MapGet("/status", async context =>
{
    await context.Response.WriteAsync("OK");
});


/*
app.MapGet("/errors", () =>
{
    return Results.Ok(ErrorUtils.GetGroupedErrorConstants(typeof(ScreeningErrors), typeof(ScreeningScheduleErrors)));
})
.WithName("Posible Errors")
.WithTags("Errors Reference");
*/

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.Servers = [
        new ScalarServer("https://localhost:7090", "Audit Service"),
        new ScalarServer("https://localhost:7000/api", "API Gateway")
    ];
});


app.Run();
