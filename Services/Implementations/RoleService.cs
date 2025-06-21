using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class RoleService : IRoleService
    {
        private readonly VinylContext _context;

        public RoleService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RoleDTO>> GetAllAsync()
        {
            return await _context.Role
                .Select(r => new RoleDTO
                {
                    RoleId = r.RoleId,
                    RoleName = r.RoleName
                })
                .ToListAsync();
        }

        public async Task<RoleDTO?> GetByIdAsync(int id)
        {
            var r = await _context.Role.FindAsync(id);
            return r == null ? null : new RoleDTO
            {
                RoleId = r.RoleId,
                RoleName = r.RoleName
            };
        }

        public async Task<RoleDTO> CreateAsync(RoleDTO dto)
        {
            var entity = new Role
            {
                RoleName = dto.RoleName
            };

            _context.Role.Add(entity);
            await _context.SaveChangesAsync();

            dto.RoleId = entity.RoleId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, RoleDTO dto)
        {
            var entity = await _context.Role.FindAsync(id);
            if (entity == null) return false;

            entity.RoleName = dto.RoleName;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Role.FindAsync(id);
            if (entity == null) return false;

            _context.Role.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
