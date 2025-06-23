using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketDTO>> GetAllBaskets(int page, int limit);
        Task<BasketDTO?> GetBasketById(int id);
        Task<BasketDTO> CreateBasket(BasketDTO dto);
        Task<bool> UpdateBasket(int id, BasketDTO dto);
        Task<bool> DeleteBasket(int id);
    }
}
