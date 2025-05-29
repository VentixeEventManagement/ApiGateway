using ApiGateway.Infrastructure.Documentation;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

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
    .AddSwaggerConfiguration(builder.Configuration) // This is from our SwaggerConfiguration class
    .AddOcelot(builder.Configuration);

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerConfiguration(builder.Configuration);
}

await app.UseOcelot();

app.Run();