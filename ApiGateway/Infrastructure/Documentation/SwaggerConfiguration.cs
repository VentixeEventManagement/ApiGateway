using ApiGateway.Infrastructure.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.Configuration;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Net.Http;

namespace ApiGateway.Infrastructure.Documentation;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Add endpoints API explorer
        services.AddEndpointsApiExplorer();

        // Add standard Swagger for gateway's own endpoints
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API Gateway",
                Version = "v1",
                Description = "API Gateway endpoints"
            });
        });

        // Register ApiKeyMessageHandler as a singleton so it can be used in HttpClient factory
        services.AddSingleton<ApiKeyHandler>();

        // Configure custom HttpClient for Swagger
        services.AddHttpClient("SwaggerForOcelot", client =>
        {
            client.DefaultRequestHeaders.Add("X-API-KEY", "34023b33-ab56-4925-add5-03666cf294a3");
        })
        .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        });

        // Configure and add SwaggerForOcelot
        services.AddSwaggerForOcelot(configuration);

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IConfiguration configuration)
    {
        // Use standard Swagger middleware first
        app.UseSwagger();

        // Standard Swagger UI for gateway's own endpoints
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");
            options.RoutePrefix = "gateway-docs";
        });

        // Configure Swagger for Ocelot with multiple selectable endpoints
        // Using the simplest overload that should work with version 8.1.0
        app.UseSwaggerForOcelotUI(configuration);

        return app;
    }
}