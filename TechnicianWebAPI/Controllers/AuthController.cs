using Microsoft.AspNetCore.Mvc;
using TechnicianWebAPI.DTOs;
using TmsWebApi.Services;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace TmsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    success = false,
                    errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)
                });
            }

            var result = await _authService.LoginAsync(request);

            if (!result.Success)
            {
                return Unauthorized(new { success = false, message = result.Message });
            }

            return Ok(new
            {
                token = result.Token,
                success = true,
                username = result.Username,
                role = result.Role,
                message = result.Message
            });
        }
    }
}
