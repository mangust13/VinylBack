using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly VinylContext _context;

        public AppUserService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AppUserDTO>> GetAllAsync()
        {
            return await _context.AppUser
                .Select(u => new AppUserDTO
                {
                    UserId = u.UserId,
                    UserFullName = u.UserFullName,
                    PhoneNumber = u.PhoneNumber,
                    Email = u.Email,
                    PasswordHash = u.PasswordHash,
                    PasswordSalt = u.PasswordSalt,
                    RoleId = u.RoleId
                })
                .ToListAsync();
        }

        public async Task<AppUserDTO?> GetByIdAsync(int id)
        {
            var u = await _context.AppUser.FindAsync(id);
            return u == null ? null : new AppUserDTO
            {
                UserId = u.UserId,
                UserFullName = u.UserFullName,
                PhoneNumber = u.PhoneNumber,
                Email = u.Email,
                PasswordHash = u.PasswordHash,
                PasswordSalt = u.PasswordSalt,
                RoleId = u.RoleId
            };
        }

        public async Task<AppUserDTO> CreateAsync(AppUserDTO dto)
        {
            var entity = new AppUser
            {
                UserFullName = dto.UserFullName,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                PasswordHash = dto.PasswordHash,
                PasswordSalt = dto.PasswordSalt,
                RoleId = dto.RoleId
            };

            _context.AppUser.Add(entity);
            await _context.SaveChangesAsync();

            dto.UserId = entity.UserId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, AppUserDTO dto)
        {
            var entity = await _context.AppUser.FindAsync(id);
            if (entity == null) return false;

            entity.UserFullName = dto.UserFullName;
            entity.PhoneNumber = dto.PhoneNumber;
            entity.Email = dto.Email;
            entity.PasswordHash = dto.PasswordHash;
            entity.PasswordSalt = dto.PasswordSalt;
            entity.RoleId = dto.RoleId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AppUser.FindAsync(id);
            if (entity == null) return false;

            _context.AppUser.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
