using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketDTO>> GetAllAsync();
        Task<BasketDTO?> GetByIdAsync(int id);
        Task<BasketDTO> CreateAsync(BasketDTO dto);
        Task<bool> UpdateAsync(int id, BasketDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
