using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUserDTO>> GetAllUsers(int page, int limit);
        Task<AppUserDTO?> GetUserById(int id);
        Task<AppUserDTO> CreateUser(AppUserDTO dto);
        Task<bool> UpdateUser(int id, AppUserDTO dto);
        Task<bool> DeleteUser(int id);
    }
}
