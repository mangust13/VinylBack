using VinylBack.Models;

namespace VinylBack.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}
