using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class TrackInBasketService : ITrackInBasketService
    {
        private readonly VinylContext _context;

        public TrackInBasketService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrackInBasketDTO>> GetAllAsync()
        {
            return await _context.TrackInBasket
                .Select(t => new TrackInBasketDTO
                {
                    TrackInBasketId = t.TrackInBasketId,
                    BasketId = t.BasketId,
                    TrackId = t.TrackId
                })
                .ToListAsync();
        }

        public async Task<TrackInBasketDTO?> GetByIdAsync(int id)
        {
            var t = await _context.TrackInBasket.FindAsync(id);
            return t == null ? null : new TrackInBasketDTO
            {
                TrackInBasketId = t.TrackInBasketId,
                BasketId = t.BasketId,
                TrackId = t.TrackId
            };
        }

        public async Task<TrackInBasketDTO> CreateAsync(TrackInBasketDTO dto)
        {
            var entity = new TrackInBasket
            {
                BasketId = dto.BasketId,
                TrackId = dto.TrackId
            };

            _context.TrackInBasket.Add(entity);
            await _context.SaveChangesAsync();

            dto.TrackInBasketId = entity.TrackInBasketId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, TrackInBasketDTO dto)
        {
            var entity = await _context.TrackInBasket.FindAsync(id);
            if (entity == null) return false;

            entity.BasketId = dto.BasketId;
            entity.TrackId = dto.TrackId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.TrackInBasket.FindAsync(id);
            if (entity == null) return false;

            _context.TrackInBasket.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
