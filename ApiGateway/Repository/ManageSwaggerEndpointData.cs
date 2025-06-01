// This document was formatted and refined by AI, also copied base from demo Repo for MMLIB SwaggerForOcelot cause i couldnt get it to work on my own
using ApiGateway.Interceptor;

namespace ApiGateway.Repository
{
    /// <summary>
    /// Represents configuration data for managing Swagger endpoints in the API gateway.
    /// </summary>
    /// <remarks>
    /// This class is used by the <see cref="PublishedDownstreamInterceptor"/> to control
    /// which Swagger endpoints are accessible to API consumers based on publication status.
    /// </remarks>
    public class ManageSwaggerEndpointData
    {
        /// <summary>
        /// Gets or sets a value indicating whether the Swagger endpoint is published and accessible.
        /// </summary>
        /// <value>
        /// <c>true</c> if the endpoint is published and should be accessible; otherwise, <c>false</c>.
        /// When <c>false</c>, the endpoint will return a 404 status code with a message indicating 
        /// that it is under development.
        /// </value>
        public bool IsPublished { get; set; }
    }
}