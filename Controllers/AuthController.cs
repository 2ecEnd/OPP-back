using Microsoft.AspNetCore.Mvc;
using OPP_back.Models.Data;
using OPP_back.Models.Dto;
using OPP_back.Models.Dto.Requests;
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
            var tokens = await _AuthService.RefreshTokens(token.Token);

            if (tokens == null)
                return Unauthorized();

            return Ok(tokens);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser([FromBody] RefreshTokenRequest token)
        {
            if (await _AuthService.LogoutUser(token.Token))
                return Ok();
            return Unauthorized();
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetUser()
        {
            var sessionClaim = User.FindFirst("session");
            if (sessionClaim == null)
                return Unauthorized();

            if (!Guid.TryParse(sessionClaim.Value, out var userId))
                return Unauthorized();

            var user = await _AuthService.GetUser(userId);
            if (user == null)
                return Unauthorized();

            return Ok(user);
        }

        [HttpPut("save")]
        public async Task<IActionResult> ChangeUser([FromBody] UserDto user)
        {
            var sessionClaim = User.FindFirst("session");
            if (sessionClaim == null)
                return Unauthorized();

            if (!Guid.TryParse(sessionClaim.Value, out var userId))
                return Unauthorized();

            if (!await _AuthService.ChangeUser(user))
                return Unauthorized();

            return Ok();
        }
    }
}
