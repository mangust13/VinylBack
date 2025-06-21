using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IAppUserService
    {
        Task<IEnumerable<AppUserDTO>> GetAllAsync();
        Task<AppUserDTO?> GetByIdAsync(int id);
        Task<AppUserDTO> CreateAsync(AppUserDTO dto);
        Task<bool> UpdateAsync(int id, AppUserDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
