namespace OPP_back.Services.Interfaces
{
    public interface IPasswordHashService
    {
        public string HashPassword(string password);
        public bool VerifyPassword(string password, string passwordHash);
    }
}
