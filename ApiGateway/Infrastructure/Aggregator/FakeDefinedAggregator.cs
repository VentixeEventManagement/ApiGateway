// ApiGateway/Infrastructure/Aggregators/FakeDefinedAggregator.cs
using Ocelot.Middleware;
using Ocelot.Multiplexer;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ApiGateway.Infrastructure.Aggregators;

public class FakeDefinedAggregator : IDefinedAggregator
{
    public async Task<DownstreamResponse> Aggregate(List<HttpContext> responses)
    {
        var aggregatedContent = new Dictionary<string, object>();

        foreach (var response in responses)
        {
            if (response?.Items != null)
            {
                var downstreamResponse = response.Items.DownstreamResponse();
                var content = await downstreamResponse.Content.ReadAsStringAsync();

                // Use the key from ocelot.json to identify the response
                var routeKey = response.Items.DownstreamRoute().Key;
                aggregatedContent.Add(routeKey, content);
            }
        }

        var stringContent = System.Text.Json.JsonSerializer.Serialize(aggregatedContent);
        var httpContent = new StringContent(stringContent)
        {
            Headers = { ContentType = new MediaTypeHeaderValue("application/json") }
        };

        return new DownstreamResponse(httpContent, HttpStatusCode.OK, new List<KeyValuePair<string, IEnumerable<string>>>(), "OK");
    }
}