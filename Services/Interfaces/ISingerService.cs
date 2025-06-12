using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public interface ISingerService
    {
        Task<IEnumerable<SingerDTO>> GetAllSingersAsync();
    }
}
