using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class CountryService : ICountryService
    {
        private readonly VinylContext _context;

        public CountryService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CountryDTO>> GetAllAsync()
        {
            return await _context.Country
                .Select(c => new CountryDTO
                {
                    CountryId = c.CountryId,
                    CountryName = c.CountryName
                })
                .ToListAsync();
        }

        public async Task<CountryDTO?> GetByIdAsync(int id)
        {
            var c = await _context.Country.FindAsync(id);
            return c == null ? null : new CountryDTO
            {
                CountryId = c.CountryId,
                CountryName = c.CountryName
            };
        }

        public async Task<CountryDTO> CreateAsync(CountryDTO dto)
        {
            var entity = new Country
            {
                CountryName = dto.CountryName
            };

            _context.Country.Add(entity);
            await _context.SaveChangesAsync();

            dto.CountryId = entity.CountryId;
            return dto;
        }

        public async Task<bool> UpdateAsync(int id, CountryDTO dto)
        {
            var entity = await _context.Country.FindAsync(id);
            if (entity == null) return false;

            entity.CountryName = dto.CountryName;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Country.FindAsync(id);
            if (entity == null) return false;

            _context.Country.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
