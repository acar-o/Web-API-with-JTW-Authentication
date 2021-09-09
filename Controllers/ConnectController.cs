using Microsoft.AspNetCore.Mvc;
using Net5.Sample.API.Auth;
using Net5.Sample.API.Models;

namespace Net5.Sample.API.Controllers
{
    /// <summary>
    /// Used to get JWT token for authentication
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ConnectController : ControllerBase
    {
        private readonly IAuth authService;

        public ConnectController(IAuth p_authService) => authService = p_authService;

        /// <summary>
        /// Method provides a JWT Token valid for 30 minuten on providing valid credentials 
        /// </summary>
        /// <param name="apiCredentials">API Key and Secret</param>
        /// <returns>JWT Token</returns>
        /// <response code="200">JWT Token valid for 30 minutes</response>
        /// <response code="401">Invalid API Credentials</response>  
        [HttpPost]
        //[Route("Authorize")]
        public IActionResult Authorize([FromBody] ApiCredentials apiCredentials)
        {
            var token = authService.Authenticate(apiCredentials.ApiKey, apiCredentials.ApiSecret);

            return string.IsNullOrWhiteSpace(token) ? Unauthorized() : Ok(token);
        }
    }
}
