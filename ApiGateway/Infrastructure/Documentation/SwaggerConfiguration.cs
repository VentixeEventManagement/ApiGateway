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

        // Configure and add SwaggerForOcelot
        services.AddSwaggerForOcelot(configuration);

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, IConfiguration configuration)
    {
        // Use standard Swagger middleware
        app.UseSwagger();

        // Configure Swagger for Ocelot with unified endpoint
        app.UseSwaggerForOcelotUI(options =>
        {
            options.PathToSwaggerGenerator = "/swagger/docs";
        });

        return app;
    }
}