using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ILocationService
    {
        Task<IEnumerable<LocationDTO>> GetAllAsync();
        Task<LocationDTO?> GetByIdAsync(int id);
        Task<LocationDTO> CreateAsync(LocationDTO dto);
        Task<bool> UpdateAsync(int id, LocationDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
