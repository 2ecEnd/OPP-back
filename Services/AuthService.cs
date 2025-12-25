using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OPP_back.Models.Data;
using OPP_back.Models.Dto;
using OPP_back.Models.Dto.Responses;
using OPP_back.Services.Interfaces;
using System.Collections.Specialized;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OPP_back.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _DbContext;
        private readonly IPasswordHashService _PasswordHasher;
        private readonly TokenService _TokenService;

        static int cnt = 0;

        public AuthService(
            AppDbContext DbContext,
            IPasswordHashService PasswordHasher,
            TokenService TokenService
        ) {
            _DbContext = DbContext;
            _PasswordHasher = PasswordHasher;
            _TokenService = TokenService;
        }

        public async Task<TokensResponseDto?> RegisterUser(string email, string password)
        {
            if (await _DbContext.Users.AnyAsync(u => u.Email == email))
                return null;

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = email,
                PasswordHash = _PasswordHasher.HashPassword(password),
                Subjects = [],
                Teams = []
            };

            await _DbContext.Users.AddAsync(user);
            await _DbContext.SaveChangesAsync();

            var tokens = GenerateTokens(user);

            return tokens;
        }

        public async Task<TokensResponseDto?> LoginUser(string email, string password)
        {
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null ||
                !_PasswordHasher.VerifyPassword(password, user.PasswordHash))
                return null;

            var tokens = GenerateTokens(user);

            await _DbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = tokens.Refresh,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsValid = true,
                UserId = user.Id,
                User = user
            });
            await _DbContext.SaveChangesAsync();

            return tokens;
        }

        public async Task<TokensResponseDto?> RefreshTokens(string token)
        {
            var refToken = await _DbContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
            if (refToken == null ||
                refToken.ExpiresAt < DateTime.UtcNow ||
                !refToken.IsValid)
                return null;

            refToken.IsValid = false;
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Id == refToken.UserId);
            if (user == null)
                return null;

            var tokens = GenerateTokens(user);

            await _DbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                Id = Guid.NewGuid(),
                Token = tokens.Refresh,
                ExpiresAt = DateTime.UtcNow.AddHours(1),
                IsValid = true,
                UserId = user.Id,
                User = user
            });
            await _DbContext.SaveChangesAsync();

            return tokens;
        }

        public async Task<bool> LogoutUser(string token)
        {
            var refToken = await _DbContext.RefreshTokens.FirstOrDefaultAsync(t => t.Token == token);

            if (refToken == null ||
                refToken.ExpiresAt < DateTime.UtcNow ||
                !refToken.IsValid) 
                return false;

            refToken.IsValid = false;
            await _DbContext.SaveChangesAsync();

            return true;
        }

        private TokensResponseDto GenerateTokens(User user)
        {
            return new TokensResponseDto
            {
                Access = _TokenService.GenerateAccessToken(user),
                Refresh = _TokenService.GenerateRefreshToken()
            };
        }
    }
}
