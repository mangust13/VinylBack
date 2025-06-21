using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllAsync();
        Task<RoleDTO?> GetByIdAsync(int id);
        Task<RoleDTO> CreateAsync(RoleDTO dto);
        Task<bool> UpdateAsync(int id, RoleDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
