using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class SingerService : ISingerService
    {
        private readonly VinylContext _context;
        public SingerService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SingerDto>> GetAllSingersAsync()
        {
            return await _context.Singer
                .Select(s => new SingerDto
                {
                    SingerId = s.SingerId,
                    SingerFullName = s.SingerFullName,
                    SingerURL = s.SingerURL
                })
                .ToListAsync();
        }

        public async Task<SingerDto?> GetSingerByIdAsync(int id)
        {
            var s = await _context.Singer.FindAsync(id);
            return s == null ? null : new SingerDto
            {
                SingerId = s.SingerId,
                SingerFullName = s.SingerFullName,
                SingerURL = s.SingerURL
            };
        }

        public async Task<SingerDto?> CreateSingerAsync(SingerDto singer)
        {
            var newSinger = new Singer
            {
                SingerFullName = singer.SingerFullName,
                SingerURL = singer.SingerURL
            };

            _context.Singer.Add(newSinger);
            await _context.SaveChangesAsync();

            singer.SingerId = newSinger.SingerId;
            return singer;
        }

        
        public async Task<bool> UpdateSingerAsync(int id, SingerDto singer)
        {
            var updatedSinger = await _context.Singer.FindAsync(id);
            if (updatedSinger == null)
                return false;

            updatedSinger.SingerFullName = singer.SingerFullName;
            updatedSinger.SingerURL = singer.SingerURL;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSingerAsync(int id)
        {
            var singer = await _context.Singer.FindAsync(id);
            if (singer == null)
                return false;

            _context.Singer.Remove(singer);
            await _context.SaveChangesAsync();
            return true;
        }        
    }
}
