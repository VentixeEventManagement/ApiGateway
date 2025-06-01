// This document was formatted and refined by AI, also copied base from demo Repo for MMLIB SwaggerForOcelot cause i couldnt get it to work on my own
using ApiGateway.Repository; 
using Microsoft.AspNetCore.Http; 
using MMLib.SwaggerForOcelot.Configuration; 
using MMLib.SwaggerForOcelot.Middleware;


namespace ApiGateway.Interceptor
{
    public class PublishedDownstreamInterceptor : ISwaggerDownstreamInterceptor
    {
        private readonly ISwaggerEndpointConfigurationRepository _endpointConfigurationRepository;

        public PublishedDownstreamInterceptor(ISwaggerEndpointConfigurationRepository endpointConfigurationRepository)
        {
            _endpointConfigurationRepository = endpointConfigurationRepository;
        }

        public bool DoDownstreamSwaggerEndpoint(HttpContext httpContext, string version, SwaggerEndPointOptions endPoint)
        {
            var myEndpointConfiguration = _endpointConfigurationRepository.GetSwaggerEndpoint(endPoint, version);

            if (!myEndpointConfiguration.IsPublished)
            {
                httpContext.Response.StatusCode = 404;
                httpContext.Response.WriteAsync("This enpoint is under development, please come back later.");
            }

            return myEndpointConfiguration.IsPublished;
        }
    }
}