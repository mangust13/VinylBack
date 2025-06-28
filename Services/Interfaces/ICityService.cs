using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ICityService
    {
        Task<PagedResultDto<CityDTO>> GetAllCities(int page, int limit);

        Task<CityDTO?> GetCityById(int id);
        Task<CityDTO> CreateCity(CityDTO dto);
        Task<bool> UpdateCity(int id, CityDTO dto);
        Task<bool> DeleteCity(int id);
    }
}
