﻿using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<TrackInBasketDTO>> GetAllTrackInBasketServices()
        {
            return await _context.TrackInBasket
                .Select(t => new TrackInBasketDTO
                {
                    TrackInBasketId = t.TrackInBasketId,
                    TrackCount = t.TrackCount,
                    BasketId = t.BasketId,
                    TrackId = t.TrackId
                })
                .ToListAsync();
        }

        public async Task<TrackInBasketDTO?> GetTrackInBasketServiceById(int id)
        {
            var t = await _context.TrackInBasket.FindAsync(id);
            return t == null ? null : new TrackInBasketDTO
            {
                TrackInBasketId = t.TrackInBasketId,
                TrackCount = t.TrackCount,
                BasketId = t.BasketId,
                TrackId = t.TrackId
            };
        }

        public async Task<TrackInBasketDTO> CreateTrackInBasketService(TrackInBasketDTO dto)
        {
            var entity = new TrackInBasket
            {
                TrackCount = dto.TrackCount,
                BasketId = dto.BasketId,
                TrackId = dto.TrackId
            };

            _context.TrackInBasket.Add(entity);
            await _context.SaveChangesAsync();

            dto.TrackInBasketId = entity.TrackInBasketId;
            return dto;
        }

        public async Task<bool> UpdateTrackInBasketService(int id, TrackInBasketDTO dto)
        {
            var entity = await _context.TrackInBasket.FindAsync(id);
            if (entity == null) return false;

            entity.BasketId = dto.BasketId;
            entity.TrackId = dto.TrackId;
            entity.TrackCount = dto.TrackCount;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTrackInBasketService(int id)
        {
            var entity = await _context.TrackInBasket.FindAsync(id);
            if (entity == null) return false;

            _context.TrackInBasket.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
