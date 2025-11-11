using OPP_back.Models.Dto;

namespace OPP_back.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Guid?> RegisterUser(string email, string password);
        public Task<TokensDto?> LoginUser(string email, string password);
        public Task<TokensDto?> RefreshTokens(string token);
        public Task<bool> LogoutUser(string token);
    }
}
