using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDTO>> GetAllRoles();
        Task<RoleDTO?> GetRoleById(int id);
        Task<RoleDTO> CreateRole(RoleDTO dto);
        Task<bool> UpdateRole(int id, RoleDTO dto);
        Task<bool> DeleteRole(int id);
    }
}
