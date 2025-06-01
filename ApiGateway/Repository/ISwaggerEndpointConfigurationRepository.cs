// This document was formatted and refined by AI, also copied base from demo Repo for MMLIB SwaggerForOcelot cause i couldnt get it to work on my own
using ApiGateway.Repository;
using MMLib.SwaggerForOcelot.Configuration;

namespace ApiGateway.Repository
{
    /// <summary>
    /// Defines a repository interface for retrieving Swagger endpoint configuration data.
    /// </summary>
    /// <remarks>
    /// This interface is used by the API gateway to determine which Swagger endpoints 
    /// should be exposed in the UI based on their publication status.
    /// </remarks>
    public interface ISwaggerEndpointConfigurationRepository
    {
        /// <summary>
        /// Retrieves the publication status and related configuration for a specified Swagger endpoint and version.
        /// </summary>
        /// <param name="endPoint">The Swagger endpoint options containing configuration data such as key identifier.</param>
        /// <param name="version">The version of the API endpoint to retrieve configuration for.</param>
        /// <returns>
        /// A <see cref="ManageSwaggerEndpointData"/> object containing endpoint publication status and other management information.
        /// </returns>
        ManageSwaggerEndpointData GetSwaggerEndpoint(SwaggerEndPointOptions endPoint, string version);
    }
}