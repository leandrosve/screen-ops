var builder = DistributedApplication.CreateBuilder(args);

var apiGateway = builder.AddProject<Projects.ApiGateway>("ApiGateway");

var authService = builder.AddProject<Projects.AuthenticationService>("AuthenticationService");

var notificationService = builder.AddProject<Projects.NotificationService>("NotificationService");

builder.Build().Run();
