// This document was formatted and refined by AI, also copied base from demo Repo for MMLIB SwaggerForOcelot cause i couldnt get it to work on my own
using ApiGateway.Repository;
using MMLib.SwaggerForOcelot.Configuration;

namespace ApiGateway.Repository
{
    public interface ISwaggerEndpointConfigurationRepository
    {
        ManageSwaggerEndpointData GetSwaggerEndpoint(SwaggerEndPointOptions endPoint, string version);
    }
}
