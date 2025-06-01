// This document was formatted and refined by AI
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiGateway.Security
{
    /// <summary>
    /// Provides extension methods for configuring JWT-based authentication and Swagger security in the API gateway.
    /// </summary>
    /// <remarks>
    /// This service facilitates the setup of JWT authentication with Bearer token scheme and configures
    /// Swagger documentation to support JWT authentication for API testing.
    /// </remarks>
    public static class JwtAuthenticationService
    {
        /// <summary>
        /// Configures JWT Bearer token authentication for the API gateway service.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the authentication services to.</param>
        /// <param name="configuration">The <see cref="IConfiguration"/> containing JWT settings from appsettings.json.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> with configured JWT authentication.</returns>
        /// <remarks>
        /// This method:
        /// <list type="bullet">
        /// <item><description>Sets up JWT Bearer authentication with validation parameters</description></item>
        /// <item><description>Configures token validation including issuer, audience, and lifetime validation</description></item>
        /// <item><description>Adds event handlers for authentication failures and token message processing</description></item>
        /// <item><description>Defines authorization policies for role-based access control</description></item>
        /// </list>
        /// Required configuration in appsettings.json:
        /// <code>
        /// "JWT": {
        ///    "Secret": "your-secret-key",
        ///    "Issuer": "issuer-name",
        ///    "Audience": "audience-name"
        /// }
        /// </code>
        /// </remarks>
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]!)),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWT:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["JWT:Audience"],
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5),
                    NameClaimType = JwtRegisteredClaimNames.Sub,
                    RoleClaimType = ClaimTypes.Role
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine($"JWT authentication failed: {context.Exception.Message}");
                        return Task.CompletedTask;
                    },
                    OnMessageReceived = context =>
                    {
                        var authHeader = context.Request.Headers["Authorization"].ToString();
                        Console.WriteLine($"Authorization header: {authHeader}");

                        if (authHeader.StartsWith("Bearer Bearer "))
                        {
                            var correctedHeader = authHeader.Replace("Bearer Bearer ", "Bearer ");
                            context.Request.Headers["Authorization"] = correctedHeader;
                            Console.WriteLine($"Corrected Authorization header: {correctedHeader}");
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("AuthenticatedUser", policy =>
                    policy.RequireAuthenticatedUser());
            });

            return services;
        }

        /// <summary>
        /// Configures Swagger to support JWT authentication for API documentation and testing.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the Swagger configuration services to.</param>
        /// <returns>The updated <see cref="IServiceCollection"/> with configured Swagger security.</returns>
        /// <remarks>
        /// This method:
        /// <list type="bullet">
        /// <item><description>Creates the API documentation with version information</description></item>
        /// <item><description>Adds JWT Bearer token security definition to Swagger</description></item>
        /// <item><description>Configures Swagger UI to display the authorization dialog</description></item>
        /// </list>
        /// After configuration, users can authenticate in the Swagger UI by providing a JWT token
        /// in the format: "Bearer {token}".
        /// </remarks>
        public static IServiceCollection AddJwtSwaggerSecurityConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });

            return services;
        }
    }
}
