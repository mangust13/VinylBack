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

        public async Task<PagedResultDto<CountryDto>> GetAllCountries(int page, int limit)
        {
            var query = _context.Country.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(c => new CountryDto
                {
                    CountryId = c.CountryId,
                    CountryName = c.CountryName
                })
                .ToListAsync();

            return new PagedResultDto<CountryDto>
            {
                TotalCount = totalCount,
                Items = items
            };
        }


        public async Task<CountryDto?> GetCountryById(int id)
        {
            var c = await _context.Country.FindAsync(id);
            return c == null ? null : new CountryDto
            {
                CountryId = c.CountryId,
                CountryName = c.CountryName
            };
        }

        public async Task<CountryDto> CreateCountry(CountryDto dto)
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

        public async Task<bool> UpdateCountry(int id, CountryDto dto)
        {
            var entity = await _context.Country.FindAsync(id);
            if (entity == null) return false;

            entity.CountryName = dto.CountryName;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteCountry(int id)
        {
            var entity = await _context.Country.FindAsync(id);
            if (entity == null) return false;

            _context.Country.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
