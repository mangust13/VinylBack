using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ISingerService
    {
        Task<IEnumerable<SingerDto>> GetAllSingersAsync();
        Task<SingerDto?> GetSingerByIdAsync(int id);
        Task<SingerDto?> CreateSingerAsync(SingerDto singer);
        Task<bool> UpdateSingerAsync(int id, SingerDto singer);
        Task<bool> DeleteSingerAsync(int id);
    }
}
