using POS.Entity.Person;

namespace POS.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, ICollection<string> roles);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}