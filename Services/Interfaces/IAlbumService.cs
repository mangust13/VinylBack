using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IAlbumService
    {
        Task<PagedResultDto<AlbumDto>> GetAllAlbums(
            int page, int limit,
            List<int>? singerIds = null,
            List<int>? genreIds = null,
            List<int>? styleIds = null,
            List<int>? lableIds = null,
            List<int>? countryIds = null,
            int? minYear = null,
            int? maxYear = null,
            string? sortByYear = null);

        Task<AlbumDto?> GetAlbumById(int id);
        Task<IEnumerable<object>> GetAlbumsByGenresAndStyles(List<int>? genreIds, List<int>? styleIds);
        Task<AlbumDto> CreateAlbum(AlbumDto album);
        Task<bool> UpdateAlbum(int id, AlbumDto album);
        Task<bool> DeleteAlbum(int id);
    }
}
