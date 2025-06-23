using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ISingerService
    {
        Task<IEnumerable<SingerDto>> GetAllSingers(int page, int limit);
        Task<SingerDto?> GetSingerById(int id);
        Task<SingerDto?> CreateSinger(SingerDto singer);
        Task<bool> UpdateSinger(int id, SingerDto singer);
        Task<bool> DeleteSinger(int id);
    }
}
