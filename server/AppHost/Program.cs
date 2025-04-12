var builder = DistributedApplication.CreateBuilder(args);

var apiGateway = builder.AddProject<Projects.ApiGateway>("ApiGateway");

var authService = builder.AddProject<Projects.AuthenticationService>("AuthenticationService");

var notificationService = builder.AddProject<Projects.NotificationService>("NotificationService");

var cinemasService = builder.AddProject<Projects.CinemasService>("CinemaService");

var screeningsService = builder.AddProject<Projects.ScreeningsService>("ScreeningsService");

var moviesService = builder.AddProject<Projects.MoviesService>("MoviesService");

builder.Build().Run();
