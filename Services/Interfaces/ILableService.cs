using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ILableService
    {
        Task<PagedResultDto<LableDto>> GetAllLables(int page, int limit);
        Task<LableDto?> GetByIdLable(int id);
        Task<LableDto> CreateLable(LableDto lableDto);
        Task<LableDto?> UpdateLable(int id, LableDto lableDto);
        Task<bool> DeleteLable(int id);
    }
}
