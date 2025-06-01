// This document was formatted and refined by AI
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.Controllers
{
    [Route("api/gateway")]
    [ApiController]
    [Authorize(Roles = "Admin")]  
    public class GatewayController : ControllerBase
    {
        [HttpGet("health")]
        public ActionResult<bool> Health()
        {
            return Ok(true);
        }

        [HttpGet("public-health")]
        [AllowAnonymous] 
        public ActionResult<string> PublicHealth()
        {
            return Ok("Gateway API is working - no authentication required");
        }

    }


}