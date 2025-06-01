// Generated with AI
using ApiGateway.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace ApiGateway.Tests
{
    public class GatewayControllerTests
    {
        [Fact]
        public void Health_ShouldReturnTrue()
        {
            // Arrange
            var controller = new GatewayController();

            // Act
            var result = controller.Health();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<bool>(okResult.Value);
            Assert.True(returnValue);
        }

        [Fact]
        public void PublicHealth_ShouldReturnCorrectMessage()
        {
            // Arrange
            var controller = new GatewayController();

            // Act
            var result = controller.PublicHealth();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<string>(okResult.Value);
            Assert.Equal("Gateway API is working - no authentication required", returnValue);
        }

        [Fact]
        public void Controller_ShouldHaveAuthorizeAttribute()
        {
            // Arrange
            var controllerType = typeof(GatewayController);

            // Act
            var attributes = controllerType.GetCustomAttributes(typeof(AuthorizeAttribute), true);

            // Assert
            var authorizeAttr = Assert.Single(attributes) as AuthorizeAttribute;
            Assert.NotNull(authorizeAttr);
            Assert.Equal("Admin", authorizeAttr.Roles);
        }

        [Fact]
        public void Health_ShouldRequireAdminRole()
        {
            // Arrange
            var methodInfo = typeof(GatewayController).GetMethod("Health");

            // Act
            var attributes = methodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true);

            // Assert
            // The method doesn't have its own Authorize attribute because it inherits from the controller
            Assert.Empty(attributes);
        }

        [Fact]
        public void PublicHealth_ShouldHaveAllowAnonymous()
        {
            // Arrange
            var methodInfo = typeof(GatewayController).GetMethod("PublicHealth");

            // Act
            var attributes = methodInfo.GetCustomAttributes(typeof(AllowAnonymousAttribute), true);

            // Assert
            Assert.Single(attributes);
        }

        [Fact]
        public void Controller_ShouldHaveCorrectRouteAttribute()
        {
            // Arrange
            var controllerType = typeof(GatewayController);

            // Act
            var attributes = controllerType.GetCustomAttributes(typeof(RouteAttribute), true);

            // Assert
            var routeAttr = Assert.Single(attributes) as RouteAttribute;
            Assert.NotNull(routeAttr);
            Assert.Equal("api/gateway", routeAttr.Template);
        }

        [Fact]
        public void Health_ShouldHaveCorrectHttpMethod()
        {
            // Arrange
            var methodInfo = typeof(GatewayController).GetMethod("Health");

            // Act
            var attributes = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Assert
            var httpGetAttr = Assert.Single(attributes) as HttpGetAttribute;
            Assert.NotNull(httpGetAttr);
            Assert.Equal("health", httpGetAttr.Template);
        }

        [Fact]
        public void PublicHealth_ShouldHaveCorrectHttpMethod()
        {
            // Arrange
            var methodInfo = typeof(GatewayController).GetMethod("PublicHealth");

            // Act
            var attributes = methodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true);

            // Assert
            var httpGetAttr = Assert.Single(attributes) as HttpGetAttribute;
            Assert.NotNull(httpGetAttr);
            Assert.Equal("public-health", httpGetAttr.Template);
        }
    }
}
