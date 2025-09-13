using MHPlatform.Api.Common.Response;
using MHPlatform.Application.DTO;
using MHPlatform.Application.Interface;
using Microsoft.AspNetCore.Mvc;

namespace MHPlatform.Api.Controller
{
    [ApiController]
    [Route("api/security")]
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public SecurityController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest loginRequest)
        {
            var responseOptions = new ApiResponseOptions();
            responseOptions.Message = "Authentication successful";

            var result = await _securityService.LoginAsync(loginRequest.Username, loginRequest.Password);

            if (!result.IsAuthenticated)
            {
                responseOptions.IsSuccess = false;
                responseOptions.Message = "Invalid username or password";
                responseOptions.ErrorCode = 401;
                responseOptions.ErrorDetails = $"Unauthorized";
            }

            return Ok(ApiResponseFactory.Create(result, responseOptions));

        }
    }
}
