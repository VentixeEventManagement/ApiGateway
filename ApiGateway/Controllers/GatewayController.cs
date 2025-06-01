// This documenet was formatted and refined by AI
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    /// <summary>
    /// Gateway API controller providing health checks and management endpoints
    /// </summary>
    [Route("api/gateway")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class GatewayController : ControllerBase
    {
        /// <summary>
        /// Checks the health status of the API gateway
        /// </summary>
        /// <returns>Returns true if the gateway is operational</returns>
        /// <response code="200">Gateway is healthy</response>
        [HttpGet("health")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<bool> Health()
        {
            return Ok(true);
        }

        /// <summary>
        /// Public health check endpoint that doesn't require authentication
        /// </summary>
        /// <returns>A string message indicating the gateway is working</returns>
        /// <response code="200">Gateway is operational</response>
        [HttpGet("public-health")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> PublicHealth()
        {
            return Ok("Gateway API is working - no authentication required");
        }
    }
}