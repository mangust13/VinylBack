using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;

namespace VinylBack.Services
{
    public class SingerService : ISingerService
    {
        private readonly VinylContext _context;
        public SingerService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SingerDTO>> GetAllSingersAsync()
        {
            return await _context.Singer
                .Select(s => new SingerDTO
                {
                    SingerId = s.SingerId,
                    SingerFullName = s.SingerFullName,
                    SingerURL = s.SingerURL
                })
                .ToListAsync();
        }
    }
}
