using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IPurchaseStatusService
    {
        Task<IEnumerable<PurchaseStatusDTO>> GetAllAsync();
        Task<PurchaseStatusDTO?> GetByIdAsync(int id);
        Task<PurchaseStatusDTO> CreateAsync(PurchaseStatusDTO dto);
        Task<bool> UpdateAsync(int id, PurchaseStatusDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
