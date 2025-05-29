using ApiGateway.Repository;
using MMLib.SwaggerForOcelot.Configuration;

namespace ApiGateway.Repository
{
    public interface ISwaggerEndpointConfigurationRepository
    {
        ManageSwaggerEndpointData GetSwaggerEndpoint(SwaggerEndPointOptions endPoint, string version);
    }
}
