using Microsoft.AspNetCore.Mvc;
using OPP_back.Models.Requests;
using OPP_back.Services;
using OPP_back.Services.Interfaces;

namespace OPP_back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;

        public AuthController(IAuthService authService)
        {
            _AuthService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] AuthRequest registerData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var id = await _AuthService.RegisterUser(registerData.Email, registerData.Password);

            if (id == null)
                return Conflict();

            return Created();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] AuthRequest authData)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var tokens = await _AuthService.LoginUser(authData.Email, authData.Password);
            if (tokens == null)
                return BadRequest();

            return Created("", tokens);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshTokens([FromBody] RefreshTokenRequest token)
        {
            return Ok();
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser([FromBody] RefreshTokenRequest token)
        {
            if (await _AuthService.LogoutUser(token.Token))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
