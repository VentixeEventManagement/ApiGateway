using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ApiGateway.Infrastructure.Authentication
{
    public class ApiKeyHandler : DelegatingHandler
    {
        private const string ApiKeyHeaderName = "X-API-KEY";
        private const string ApiKeyValue = "34023b33-ab56-4925-add5-03666cf294a3"; // Replace with your actual API key or read from config

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Add API key to all requests going to the JWT Auth service
            if (!request.Headers.Contains(ApiKeyHeaderName))
            {
                request.Headers.Add(ApiKeyHeaderName, ApiKeyValue);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}