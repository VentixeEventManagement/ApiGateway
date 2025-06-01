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

      
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddJwtAuthentication(Configuration);

            
            services.AddJwtSwaggerSecurityConfiguration();




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

            
            app.UseAuthentication();
            app.UseAuthorization();

          
            app.UseSwagger();

         
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

          
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
