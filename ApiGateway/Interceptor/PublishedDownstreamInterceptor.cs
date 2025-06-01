// This document was formatted and refined by AI, also copied base from demo Repo for MMLIB SwaggerForOcelot cause i couldnt get it to work on my own
using ApiGateway.Repository;
using Microsoft.AspNetCore.Http;
using MMLib.SwaggerForOcelot.Configuration;
using MMLib.SwaggerForOcelot.Middleware;


namespace ApiGateway.Interceptor
{
    /// <summary>
    /// Intercepts downstream Swagger requests to control access based on publication status.
    /// Implements the MMLib.SwaggerForOcelot ISwaggerDownstreamInterceptor interface to filter endpoints.
    /// </summary>
    public class PublishedDownstreamInterceptor : ISwaggerDownstreamInterceptor
    {
        private readonly ISwaggerEndpointConfigurationRepository _endpointConfigurationRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishedDownstreamInterceptor"/> class.
        /// </summary>
        /// <param name="endpointConfigurationRepository">Repository that provides configuration data for Swagger endpoints.</param>
        public PublishedDownstreamInterceptor(ISwaggerEndpointConfigurationRepository endpointConfigurationRepository)
        {
            _endpointConfigurationRepository = endpointConfigurationRepository;
        }

        /// <summary>
        /// Determines whether a downstream Swagger endpoint should be accessible based on its publication status.
        /// If the endpoint is not published, returns a 404 response with an explanatory message.
        /// </summary>
        /// <param name="httpContext">The HTTP context for the current request.</param>
        /// <param name="version">The API version of the requested endpoint.</param>
        /// <param name="endPoint">Configuration options for the Swagger endpoint being accessed.</param>
        /// <returns>
        /// <c>true</c> if the endpoint is published and should be accessible; otherwise, <c>false</c>.
        /// </returns>
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
