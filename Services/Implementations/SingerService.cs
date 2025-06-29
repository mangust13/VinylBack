using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;
using VinylBack.Services;
using Microsoft.EntityFrameworkCore;


namespace VinylBack.Services
{
    public class SingerService : ISingerService
    {
        private readonly VinylContext _context;
        public SingerService(VinylContext context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<SingerDto>> GetAllSingers(
        int page,
        int limit,
        List<int>? genreIds = null,
        List<int>? styleIds = null,
        string? sortByName = null)
        {
            var query = _context.Singer
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Genre)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Style)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Lable)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.ReleaseCountry)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Tracks)
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

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(s => MapToDto(s))
                .ToListAsync();

            return new PagedResultDto<SingerDto>
            {
                TotalCount = totalCount,
                Items = items
            };
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
            var s = await _context.Singer
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Lable)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Genre)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Style)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.ReleaseCountry)
                .Include(s => s.Albums)
                    .ThenInclude(a => a.Tracks)
                .FirstOrDefaultAsync(s => s.SingerId == id);

            return s == null ? null : MapToDto(s);
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

            return await GetSingerById(newSinger.SingerId);
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

        private static SingerDto MapToDto(Singer s)
        {
            return new SingerDto
            {
                SingerId = s.SingerId,
                SingerFullName = s.SingerFullName,
                SingerURL = s.SingerURL,
                Albums = s.Albums?.Select(a => new AlbumDto
                {
                    AlbumId = a.AlbumId,
                    AlbumName = a.AlbumName,
                    ReleaseYear = a.ReleaseYear,
                    AlbumURL = a.AlbumURL,
                    Genre = a.Genre == null ? null : new GenreDto
                    {
                        GenreId = a.Genre.GenreId,
                        GenreName = a.Genre.GenreName
                    },
                    Style = a.Style == null ? null : new StyleDto
                    {
                        StyleId = a.Style.StyleId,
                        StyleName = a.Style.StyleName
                    },
                    Lable = a.Lable == null ? null : new LableDto
                    {
                        LableId = a.Lable.LableId,
                        LableName = a.Lable.LableName
                    },
                    ReleaseCountry = a.ReleaseCountry == null ? null : new CountryDto
                    {
                        CountryId = a.ReleaseCountry.CountryId,
                        CountryName = a.ReleaseCountry.CountryName
                    },
                    Tracks = a.Tracks.Select(t => new TrackDto
                    {
                        TrackId = t.TrackId,
                        TrackName = t.TrackName,
                        TrackDuration = t.TrackDuration,
                        Price = t.Price,
                        TrackURL = t.TrackURL,
                        AlbumId = t.AlbumId
                    }).ToList()
                }).ToList() ?? new()
            };
        }
    }
}