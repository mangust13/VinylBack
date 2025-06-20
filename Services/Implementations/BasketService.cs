﻿using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class BasketService : IBasketService
    {
        private readonly VinylContext _context;

        public BasketService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BasketDTO>> GetAllAsync()
        {
            return await _context.Basket
                .Select(b => new BasketDTO
                {
                    BasketId = b.BasketId,
                    UserId = b.UserId,
                    TotalCost = b.TotalCost
                })
                .ToListAsync();
        }

        public async Task<BasketDTO?> GetByIdAsync(int id)
        {
            var b = await _context.Basket.FindAsync(id);
            return b == null ? null : new BasketDTO
            {
                BasketId = b.BasketId,
                UserId = b.UserId,
                TotalCost = b.TotalCost
            };
        }

        public async Task<BasketDTO> CreateAsync(BasketDTO dto)
        {
            var entity = new Basket
            {
                UserId = dto.UserId,
                TotalCost = dto.TotalCost
            };

            _context.Basket.Add(entity);
            await _context.SaveChangesAsync();

            dto.BasketId = entity.BasketId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, BasketDTO dto)
        {
            var entity = await _context.Basket.FindAsync(id);
            if (entity == null) return false;

            entity.UserId = dto.UserId;
            entity.TotalCost = dto.TotalCost;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Basket.FindAsync(id);
            if (entity == null) return false;

            _context.Basket.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
