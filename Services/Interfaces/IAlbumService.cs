using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAllAlbumsAsync();
        Task<AlbumDto?> GetAlbumByIdAsync(int id);
        Task<AlbumDto> CreateAlbumAsync(AlbumDto album);
        Task<bool> UpdateAlbumAsync(int id, AlbumDto album);
        Task<bool> DeleteAlbumAsync(int id);
    }
}
