using VinylBack.Models;

namespace VinylBack.Services.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
