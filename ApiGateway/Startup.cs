// This document was formatted and refined by AI, also copied base from demo Repo for MMLIB SwaggerForOcelot cause i couldnt get it to work on my own
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.Extensions.Hosting;
using MMLib.SwaggerForOcelot.Middleware;
using Microsoft.OpenApi.Models;
using ApiGateway.Security;

namespace ApiGateway
{
    public class Startup
    {
        public Startup(IConfiguration config)
        {
            Configuration = config;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddJwtAuthentication(Configuration);

            // Add Swagger with JWT configuration
            services.AddJwtSwaggerSecurityConfiguration();

            // Add Swagger for your own API
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.5", new OpenApiInfo { Title = "Gateway API", Version = "v1" });
            });


            services.AddOcelot();
            services.AddSwaggerForOcelot(Configuration);

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UsePathBase("/gateway");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseRouting();

            // Authentication and authorization middleware
            app.UseAuthentication();
            app.UseAuthorization();

            // Swagger middleware
            app.UseSwagger();

            // API Gateway endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Ocelot and Swagger UI
            app.UseSwaggerForOcelotUI(opt =>
            {
                opt.DownstreamSwaggerEndPointBasePath = "/gateway/swagger/docs";
                opt.PathToSwaggerGenerator = "/swagger/docs";
            })
            .UseOcelot()
            .Wait();
        }
    }
}
