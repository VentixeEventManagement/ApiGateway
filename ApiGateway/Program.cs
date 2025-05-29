using ApiGateway.Infrastructure.Documentation;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Net;
using Microsoft.Extensions.Logging;



var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging
    .ClearProviders()
    .AddConsole()
    .AddDebug()
    .SetMinimumLevel(LogLevel.Debug);

// Configure HttpClient to accept all SSL certificates
ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

// Add JSON config files
builder.Configuration
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json", true, true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
    .AddJsonFile("ocelot.json", false, true)
    .AddEnvironmentVariables();

// Add services
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerConfiguration(builder.Configuration)
    .AddOcelot(builder.Configuration);

// Configure HttpClient for SSL certificate validation
builder.Services.AddHttpClient("OcelotHttpClient")
    .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
    });

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration(builder.Configuration);
}

await app.UseOcelot();

app.Run();