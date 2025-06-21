using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class PurchaseStatusService : IPurchaseStatusService
    {
        private readonly VinylContext _context;

        public PurchaseStatusService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseStatusDTO>> GetAllAsync()
        {
            return await _context.PurchaseStatus
                .Select(s => new PurchaseStatusDTO
                {
                    PurchaseStatusId = s.PurchaseStatusId,
                    PurchaseStatusName = s.PurchaseStatusName
                })
                .ToListAsync();
        }

        public async Task<PurchaseStatusDTO?> GetByIdAsync(int id)
        {
            var s = await _context.PurchaseStatus.FindAsync(id);
            return s == null ? null : new PurchaseStatusDTO
            {
                PurchaseStatusId = s.PurchaseStatusId,
                PurchaseStatusName = s.PurchaseStatusName
            };
        }

        public async Task<PurchaseStatusDTO> CreateAsync(PurchaseStatusDTO dto)
        {
            var entity = new PurchaseStatus
            {
                PurchaseStatusName = dto.PurchaseStatusName
            };

            _context.PurchaseStatus.Add(entity);
            await _context.SaveChangesAsync();

            dto.PurchaseStatusId = entity.PurchaseStatusId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, PurchaseStatusDTO dto)
        {
            var entity = await _context.PurchaseStatus.FindAsync(id);
            if (entity == null) return false;

            entity.PurchaseStatusName = dto.PurchaseStatusName;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.PurchaseStatus.FindAsync(id);
            if (entity == null) return false;

            _context.PurchaseStatus.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
