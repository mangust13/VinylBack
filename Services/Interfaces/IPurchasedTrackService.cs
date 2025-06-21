using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IPurchasedTrackService
    {
        Task<IEnumerable<PurchasedTrackDTO>> GetAllAsync();
        Task<PurchasedTrackDTO?> GetByIdAsync(int id);
        Task<PurchasedTrackDTO> CreateAsync(PurchasedTrackDTO dto);
        Task<bool> UpdateAsync(int id, PurchasedTrackDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
