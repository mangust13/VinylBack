using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAllAlbums(int page, int limit);
        Task<AlbumDto?> GetAlbumById(int id);
        Task<AlbumDto> CreateAlbum(AlbumDto album);
        Task<bool> UpdateAlbum(int id, AlbumDto album);
        Task<bool> DeleteAlbum(int id);
    }
}
