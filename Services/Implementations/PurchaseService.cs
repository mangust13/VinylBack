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

        public async Task<IEnumerable<PurchaseDTO>> GetAllPurchaseServices(int page, int limit)
        {
            return await _context.Purchase
                .Skip((page - 1) * limit)
                .Take(limit)
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

        public async Task<PurchaseDTO?> GetPurchaseServiceById(int id)
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

        public async Task<PurchaseDTO> CreatePurchaseService(PurchaseDTO dto)
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

        public async Task<bool> UpdatePurchaseService(int id, PurchaseDTO dto)
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

        public async Task<bool> DeletePurchaseService(int id)
        {
            var entity = await _context.Purchase.FindAsync(id);
            if (entity == null) return false;

            _context.Purchase.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
