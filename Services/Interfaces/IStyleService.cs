using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IStyleService
    {
        Task<IEnumerable<StyleDto>> GetAllStyles(int page, int limit);
        Task<StyleDto?> GetStyleById(int id);
        Task<StyleDto> CreateStyle(StyleDto styleDto);
        Task<StyleDto?> UpdateStyle(int id, StyleDto styleDto);
        Task<bool> DeleteStyle(int id);
    }
}
