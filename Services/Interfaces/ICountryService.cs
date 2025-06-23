using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDTO>> GetAllCountries(int page, int limit);
        Task<CountryDTO?> GetCountryById(int id);
        Task<CountryDTO> CreateCountry(CountryDTO dto);
        Task<bool> UpdateCountry(int id, CountryDTO dto);
        Task<bool> DeleteCountry(int id);
    }
}
