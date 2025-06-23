using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetAllLocations();
        Task<LocationDTO?> GetLocationById(int id);
        Task<LocationDTO> CreateLocation(LocationDTO dto);
        Task<bool> UpdateLocation(int id, LocationDTO dto);
        Task<bool> DeleteLocation(int id);
    }
}
