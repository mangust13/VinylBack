using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetAllCountries(int page, int limit);
        Task<CountryDto?> GetCountryById(int id);
        Task<CountryDto> CreateCountry(CountryDto dto);
        Task<bool> UpdateCountry(int id, CountryDto dto);
        Task<bool> DeleteCountry(int id);
    }
}
