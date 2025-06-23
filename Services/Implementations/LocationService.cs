using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class LocationService : ILocationService
    {
        private readonly VinylContext _context;

        public LocationService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LocationDTO>> GetAllLocations()
        {
            return await _context.Location
                .Select(l => new LocationDTO
                {
                    LocationId = l.LocationId,
                    LocationName = l.LocationName,
                    CityId = l.CityId
                })
                .ToListAsync();
        }

        public async Task<LocationDTO?> GetLocationById(int id)
        {
            var l = await _context.Location.FindAsync(id);
            return l == null ? null : new LocationDTO
            {
                LocationId = l.LocationId,
                LocationName = l.LocationName,
                CityId = l.CityId
            };
        }

        public async Task<LocationDTO> CreateLocation(LocationDTO dto)
        {
            var entity = new Location
            {
                LocationName = dto.LocationName,
                CityId = dto.CityId
            };

            _context.Location.Add(entity);
            await _context.SaveChangesAsync();

            dto.LocationId = entity.LocationId;
            return dto;
        }

        public async Task<bool> UpdateLocation(int id, LocationDTO dto)
        {
            var entity = await _context.Location.FindAsync(id);
            if (entity == null) return false;

            entity.LocationName = dto.LocationName;
            entity.CityId = dto.CityId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteLocation(int id)
        {
            var entity = await _context.Location.FindAsync(id);
            if (entity == null) return false;

            _context.Location.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
