using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ICityService
    {
        Task<IEnumerable<CityDTO>> GetAllAsync();
        Task<CityDTO?> GetByIdAsync(int id);
        Task<CityDTO> CreateAsync(CityDTO dto);
        Task<bool> UpdateAsync(int id, CityDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
