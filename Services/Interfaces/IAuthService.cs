using OPP_back.Models.Data;
using OPP_back.Models.Dto;
using OPP_back.Models.Dto.Responses;

namespace OPP_back.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<Guid?> RegisterUser(string email, string password);
        public Task<TokensResponseDto?> LoginUser(string email, string password);
        public Task<TokensResponseDto?> RefreshTokens(string token);
        public Task<bool> LogoutUser(string token);
        public Task<UserDto?> GetUser(Guid id);
        public Task<bool> ChangeUser(UserDto teamlead);
    }
}
