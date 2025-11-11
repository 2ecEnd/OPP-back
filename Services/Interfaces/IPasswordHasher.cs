namespace OPP_back.Services.Interfaces
{
    public interface IPasswordHasher
    {
        public Task<string> HashPassword(string password);
        public Task<bool> VerifyPassword(string password, string passwordHash);
    }
}
