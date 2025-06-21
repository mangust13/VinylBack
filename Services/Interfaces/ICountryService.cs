using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetAllAsync();
        Task<CountryDTO?> GetByIdAsync(int id);
        Task<CountryDTO> CreateAsync(CountryDTO dto);
        Task<bool> UpdateAsync(int id, CountryDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
