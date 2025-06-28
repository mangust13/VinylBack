using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IPurchasedTrackService
    {
        Task<PagedResultDto<PurchasedTrackDTO>> GetAllPurchasedTracks(int page, int limit);

        Task<PurchasedTrackDTO?> GetPurchasedTrackById(int id);
        Task<PurchasedTrackDTO> CreatePurchasedTrack(PurchasedTrackDTO dto);
        Task<bool> UpdatePurchasedTrack(int id, PurchasedTrackDTO dto);
        Task<bool> DeletePurchasedTrack(int id);
    }
}
