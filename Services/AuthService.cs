using OPP_back.Models.Dto;
using OPP_back.Services;
using OPP_back.Services.Interfaces;

namespace OPP_back.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _DbContext;
        private readonly IPasswordHashService _PasswordHasher;
        private readonly TokenService _TokenService;

        public AuthService(
            AppDbContext DbContext,
            IPasswordHashService PasswordHasher,
            TokenService TokenService
        ) {
            _DbContext = DbContext;
            _PasswordHasher = PasswordHasher;
            _TokenService = TokenService;
        }

        public async Task<Guid?> RegisterUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<TokensDto?> LoginUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<TokensDto?> RefreshTokens(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LogoutUser(string token)
        {
            throw new NotImplementedException();
        }
    }
}
