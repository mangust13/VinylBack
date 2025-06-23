using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ITrackInBasketService
    {
        Task<IEnumerable<TrackInBasketDTO>> GetAllTrackInBasketServices();
        Task<TrackInBasketDTO?> GetTrackInBasketServiceById(int id);
        Task<TrackInBasketDTO> CreateTrackInBasketService(TrackInBasketDTO dto);
        Task<bool> UpdateTrackInBasketService(int id, TrackInBasketDTO dto);
        Task<bool> DeleteTrackInBasketService(int id);
    }
}
