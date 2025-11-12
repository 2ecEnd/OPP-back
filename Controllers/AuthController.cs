using Microsoft.AspNetCore.Mvc;
using OPP_back.Services;
using OPP_back.Models.Requests;

namespace OPP_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly AuthService _AuthService;

        public AuthController(AuthService authService)
        {
            _AuthService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] AuthRequest registerData)
        {
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] AuthRequest authData)
        {
            return Ok();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens([FromBody] RefreshTokenRequest token)
        {
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser([FromBody] RefreshTokenRequest token)
        {
            return Ok();
        }
    }
}
