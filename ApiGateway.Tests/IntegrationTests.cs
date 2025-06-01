// Generated with AI
using System.Net;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace ApiGateway.Tests
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task Health_WithoutAuth_ReturnsUnauthorized()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/gateway/health");

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }


    }
}
