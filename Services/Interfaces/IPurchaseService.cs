using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IPurchaseService
    {
        Task<IEnumerable<PurchaseDTO>> GetAllPurchaseServices(int page, int limit);
        Task<PurchaseDTO?> GetPurchaseServiceById(int id);
        Task<PurchaseDTO> CreatePurchaseService(PurchaseDTO dto);
        Task<bool> UpdatePurchaseService(int id, PurchaseDTO dto);
        Task<bool> DeletePurchaseService(int id);
    }
}
