using OPP_back.Models.Dto;

namespace OPP_back.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto?> GetUser(Guid id);
        public Task<bool> ChangeUser(UserDto data);
    }
}
