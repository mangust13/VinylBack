﻿using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class CityService : ICityService
    {
        private readonly VinylContext _context;

        public CityService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CityDTO>> GetAllAsync()
        {
            return await _context.City
                .Select(c => new CityDTO
                {
                    CityId = c.CityId,
                    CityName = c.CityName,
                    CountryId = c.CountryId
                })
                .ToListAsync();
        }

        public async Task<CityDTO?> GetByIdAsync(int id)
        {
            var c = await _context.City.FindAsync(id);
            return c == null ? null : new CityDTO
            {
                CityId = c.CityId,
                CityName = c.CityName,
                CountryId = c.CountryId
            };
        }

        public async Task<CityDTO> CreateAsync(CityDTO dto)
        {
            var entity = new City
            {
                CityName = dto.CityName,
                CountryId = dto.CountryId
            };

            _context.City.Add(entity);
            await _context.SaveChangesAsync();

            dto.CityId = entity.CityId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, CityDTO dto)
        {
            var entity = await _context.City.FindAsync(id);
            if (entity == null) return false;

            entity.CityName = dto.CityName;
            entity.CountryId = dto.CountryId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.City.FindAsync(id);
            if (entity == null) return false;

            _context.City.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
