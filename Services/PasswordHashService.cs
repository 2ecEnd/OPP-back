using OPP_back.Services.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace OPP_back.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        public string HashPassword(string password)
        {
            return BC.HashPassword(password);
        }

        public bool VerifyPassword(string password, string passwordHash)
        {
            return BC.Verify(password, passwordHash);
        }
    }
}
