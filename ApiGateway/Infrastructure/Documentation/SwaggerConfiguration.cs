using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MMLib.SwaggerForOcelot.Configuration;

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

        // Configure and add SwaggerForOcelot
        services.AddSwaggerForOcelot(configuration);

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IConfiguration configuration)
    {
        // Use standard Swagger middleware first
        app.UseSwagger();

        // Standard Swagger UI for gateway endpoints
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");
            options.RoutePrefix = "api-docs";
        });

        // Configure Swagger for Ocelot with unified endpoint
        app.UseSwaggerForOcelotUI(options =>
        {
            options.PathToSwaggerGenerator = "/swagger/docs";

            // The following options are not available in SwaggerForOcelotUIOptions
            // options.DocumentTitle = "API Gateway Documentation";
            // options.InjectStylesheet("/swagger-ui/custom.css");
            // options.InjectJavascript("/swagger-ui/custom.js");
        });

        return app;
    }
}