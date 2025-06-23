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

        public async Task<IEnumerable<SingerDto>> GetAllSingers(
            int page,
            int limit,
            List<int>? genreIds = null,
            List<int>? styleIds = null,
            string? sortByName = null)
        {
            var query = _context.Singer
                .Include(s => s.Albums)
                .AsQueryable();

            if (genreIds != null && genreIds.Any())
            {
                query = query.Where(s => s.Albums
                    .Any(a => a.GenreId.HasValue && genreIds.Contains(a.GenreId.Value)));
            }

            if (styleIds != null && styleIds.Any())
            {
                query = query.Where(s => s.Albums
                    .Any(a => a.StyleId.HasValue && styleIds.Contains(a.StyleId.Value)));
            }

            if (!string.IsNullOrEmpty(sortByName))
            {
                query = sortByName.ToLower() switch
                {
                    "asc" => query.OrderBy(s => s.SingerFullName),
                    "desc" => query.OrderByDescending(s => s.SingerFullName),
                    _ => query
                };
            }

            var result = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(s => new SingerDto
                {
                    SingerId = s.SingerId,
                    SingerFullName = s.SingerFullName,
                    SingerURL = s.SingerURL
                })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<SingerDto>> GetSingersByGenresAndStyles(List<int>? genreIds, List<int>? styleIds)
        {
            var albumQuery = _context.Album.AsQueryable();

            if (genreIds != null && genreIds.Any())
                albumQuery = albumQuery.Where(a => a.GenreId.HasValue && genreIds.Contains(a.GenreId.Value));

            if (styleIds != null && styleIds.Any())
                albumQuery = albumQuery.Where(a => a.StyleId.HasValue && styleIds.Contains(a.StyleId.Value));

            var singerIds = await albumQuery
                .Where(a => a.SingerId.HasValue)
                .Select(a => a.SingerId.Value)
                .Distinct()
                .ToListAsync();

            return await _context.Singer
                .Where(s => singerIds.Contains(s.SingerId))
                .Select(s => new SingerDto
                {
                    SingerId = s.SingerId,
                    SingerFullName = s.SingerFullName,
                    SingerURL = s.SingerURL
                })
                .ToListAsync();
        }

        public async Task<SingerDto?> GetSingerById(int id)
        {
            var s = await _context.Singer.FindAsync(id);
            return s == null ? null : new SingerDto
            {
                SingerId = s.SingerId,
                SingerFullName = s.SingerFullName,
                SingerURL = s.SingerURL
            };
        }

        public async Task<SingerDto?> CreateSinger(SingerDto singer)
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

        
        public async Task<bool> UpdateSinger(int id, SingerDto singer)
        {
            var updatedSinger = await _context.Singer.FindAsync(id);
            if (updatedSinger == null)
                return false;

            updatedSinger.SingerFullName = singer.SingerFullName;
            updatedSinger.SingerURL = singer.SingerURL;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSinger(int id)
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
