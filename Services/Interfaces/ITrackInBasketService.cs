using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ITrackInBasketService
    {
        Task<IEnumerable<TrackInBasketDTO>> GetAllAsync();
        Task<TrackInBasketDTO?> GetByIdAsync(int id);
        Task<TrackInBasketDTO> CreateAsync(TrackInBasketDTO dto);
        Task<bool> UpdateAsync(int id, TrackInBasketDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
