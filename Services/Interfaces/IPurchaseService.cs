using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseDTO>> GetAllAsync();
        Task<PurchaseDTO?> GetByIdAsync(int id);
        Task<PurchaseDTO> CreateAsync(PurchaseDTO dto);
        Task<bool> UpdateAsync(int id, PurchaseDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
