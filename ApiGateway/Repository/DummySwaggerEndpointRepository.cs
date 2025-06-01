// This document was formatted and refined by AI, also copied base from demo Repo for MMLIB SwaggerForOcelot cause i couldnt get it to work on my own
using System.Collections.Generic;
using ApiGateway.Repository;
using MMLib.SwaggerForOcelot.Configuration;

namespace ApiGatewayWithEndpointSecurity.Repository
{
    /// <summary>
    /// Provides a dummy implementation of the <see cref="ISwaggerEndpointConfigurationRepository"/> interface
    /// for testing and demonstration purposes.
    /// </summary>
    public class DummySwaggerEndpointRepository : ISwaggerEndpointConfigurationRepository
    {
        /// <summary>
        /// Dictionary containing predefined endpoint data with their publication status.
        /// Key format is "{endpoint.Key}_{version}".
        /// </summary>
        private readonly Dictionary<string, ManageSwaggerEndpointData> _endpointDatas =
            new Dictionary<string, ManageSwaggerEndpointData>()
        {
            { "Events_v2", new ManageSwaggerEndpointData() { IsPublished = true } }
        };

        /// <summary>
        /// Retrieves the Swagger endpoint data for the specified endpoint and version.
        /// </summary>
        /// <param name="endPoint">The Swagger endpoint options containing configuration data.</param>
        /// <param name="version">The version of the endpoint to retrieve.</param>
        /// <returns>
        /// A <see cref="ManageSwaggerEndpointData"/> object containing the endpoint's publication status.
        /// If the endpoint is not found in the predefined data, returns a new instance with default values.
        /// </returns>
        public ManageSwaggerEndpointData GetSwaggerEndpoint(SwaggerEndPointOptions endPoint, string version)
        {
            var lookupKey = $"{endPoint.Key}_{version}";
            var endpointData = new ManageSwaggerEndpointData();
            if (_endpointDatas.ContainsKey(lookupKey))
            {
                endpointData = _endpointDatas[lookupKey];
            }

            return endpointData;
        }
    }
}
