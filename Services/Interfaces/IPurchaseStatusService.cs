using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IPurchaseStatusService
    {
        Task<IEnumerable<PurchaseStatusDTO>> GetPurchaseStatusServicesAll();
        Task<PurchaseStatusDTO?> GetPurchaseStatusServiceById(int id);
        Task<PurchaseStatusDTO> CreatePurchaseStatusService(PurchaseStatusDTO dto);
        Task<bool> UpdatePurchaseStatusService(int id, PurchaseStatusDTO dto);
        Task<bool> DeletePurchaseStatusService(int id);
    }
}
