var builder = DistributedApplication.CreateBuilder(args);

var apiGateway = builder.AddProject<Projects.ApiGateway>("ApiGateway");

var authService = builder.AddProject<Projects.AuthenticationService>("AuthenticationService");

var notificationService = builder.AddProject<Projects.NotificationService>("NotificationService");

var cinemasService = builder.AddProject<Projects.CinemasService>("CinemaService");

builder.AddProject<Projects.MoviesService>("moviesservice");

builder.Build().Run();
