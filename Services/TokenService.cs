using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OPP_back.Models.Data;
using OPP_back.Services.Interfaces;
using System.CodeDom.Compiler;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace OPP_back.Services
{
    public class TokenService
    {
        private readonly IOptions<AuthSettings> _Options;

        public TokenService(IOptions<AuthSettings> Options) 
        {
            _Options = Options;
        }


        public string GenerateAccessToken(Teamlead user)
        {
            var claims = new List<Claim>
            { 
                new Claim("session", user.Id.ToString())
            };

            var jwtToken = new JwtSecurityToken(
                expires: DateTime.UtcNow.Add(_Options.Value.Expires),
                claims: claims,
                signingCredentials:
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Options.Value.SecretKey)),
                        SecurityAlgorithms.HmacSha256
                    )
            );

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
