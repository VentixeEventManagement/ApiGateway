// This document was formatted and refined by AI, also copied base from demo Repo for MMLIB SwaggerForOcelot cause i couldnt get it to work on my own
using System.Collections.Generic;
using ApiGateway.Repository;
using MMLib.SwaggerForOcelot.Configuration;

namespace ApiGatewayWithEndpointSecurity.Repository
{
    public class DummySwaggerEndpointRepository : ISwaggerEndpointConfigurationRepository
    {
        private readonly Dictionary<string, ManageSwaggerEndpointData> _endpointDatas =
            new Dictionary<string, ManageSwaggerEndpointData>()
        {
            { "orders_v2", new ManageSwaggerEndpointData() { IsPublished = true } }
        };

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
