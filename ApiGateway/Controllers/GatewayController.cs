// This document was formatted and refined by AI
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    [Authorize(Roles = "Admin")]  // Restrict to Admin role only
    public class GatewayController : ControllerBase
    {
        [HttpGet("health")]
        public ActionResult<bool> Health()
        {
            return Ok(true);
        }

        // In GatewayController.cs
        [HttpGet("public-health")]
        [AllowAnonymous]  // This endpoint doesn't require authentication
        public ActionResult<string> PublicHealth()
        {
            return Ok("Gateway API is working - no authentication required");
        }

    }


}