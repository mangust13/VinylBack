using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly VinylContext _context;

        public PurchaseService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PurchaseDTO>> GetAllAsync()
        {
            return await _context.Purchase
                .Select(p => new PurchaseDTO
                {
                    PurchaseId = p.PurchaseId,
                    UserId = p.UserId,
                    PurchaseDate = p.PurchaseDate,
                    TotalAmount = p.TotalAmount,
                    StatusId = p.StatusId,
                    LocationId = p.LocationId
                })
                .ToListAsync();
        }

        public async Task<PurchaseDTO?> GetByIdAsync(int id)
        {
            var p = await _context.Purchase.FindAsync(id);
            return p == null ? null : new PurchaseDTO
            {
                PurchaseId = p.PurchaseId,
                UserId = p.UserId,
                PurchaseDate = p.PurchaseDate,
                TotalAmount = p.TotalAmount,
                StatusId = p.StatusId,
                LocationId = p.LocationId
            };
        }

        public async Task<PurchaseDTO> CreateAsync(PurchaseDTO dto)
        {
            var entity = new Purchase
            {
                UserId = dto.UserId,
                PurchaseDate = dto.PurchaseDate,
                TotalAmount = dto.TotalAmount,
                StatusId = dto.StatusId,
                LocationId = dto.LocationId
            };

            _context.Purchase.Add(entity);
            await _context.SaveChangesAsync();

            dto.PurchaseId = entity.PurchaseId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, PurchaseDTO dto)
        {
            var entity = await _context.Purchase.FindAsync(id);
            if (entity == null) return false;

            entity.UserId = dto.UserId;
            entity.PurchaseDate = dto.PurchaseDate;
            entity.TotalAmount = dto.TotalAmount;
            entity.StatusId = dto.StatusId;
            entity.LocationId = dto.LocationId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Purchase.FindAsync(id);
            if (entity == null) return false;

            _context.Purchase.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
