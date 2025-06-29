using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ISingerService
    {
        Task<PagedResultDto<SingerDto>> GetAllSingers(
         int page,
         int limit,
         List<int>? genreIds = null,
         List<int>? styleIds = null,
         string? sortByName = null);


        Task<IEnumerable<SingerDto>> GetSingersByGenresAndStyles(List<int>? genreIds, List<int>? styleIds);
        Task<SingerDto?> GetSingerById(int id);
        Task<SingerDto?> CreateSinger(SingerDto singer);
        Task<bool> UpdateSinger(int id, SingerDto singer);
        Task<bool> DeleteSinger(int id);
    }
}
