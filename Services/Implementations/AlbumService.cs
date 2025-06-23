using Microsoft.EntityFrameworkCore;
using System.Linq;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly VinylContext _context;

        public AlbumService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlbumDto>> GetAllAlbums(
            int page,
            int limit,
            List<int>? genreIds = null,
            List<int>? styleIds = null,
            List<int>? lableIds = null,
            List<int>? countryIds = null,
            int? minYear = null,
            int? maxYear = null,
            string? sortByYear = null)
        {
            var query = _context.Album
                .Include(a => a.Singer)
                .Include(a => a.ReleaseCountry)
                .Include(a => a.Lable)
                .Include(a => a.Genre)
                .Include(a => a.Style)
                .AsQueryable();

            if (genreIds != null && genreIds.Any())
                query = query.Where(a => a.GenreId.HasValue && genreIds.Contains(a.GenreId.Value));

            if (styleIds != null && styleIds.Any())
                query = query.Where(a => a.StyleId.HasValue && styleIds.Contains(a.StyleId.Value));

            if (lableIds != null && lableIds.Any())
                query = query.Where(a => a.LableId.HasValue && lableIds.Contains(a.LableId.Value));

            if (countryIds != null && countryIds.Any())
                query = query.Where(a => a.ReleaseCountryId.HasValue && countryIds.Contains(a.ReleaseCountryId.Value));


            if (minYear.HasValue)
                query = query.Where(a => a.ReleaseYear >= minYear.Value);

            if (maxYear.HasValue)
                query = query.Where(a => a.ReleaseYear <= maxYear.Value);

            if(!string.IsNullOrEmpty(sortByYear))
            {
                query = sortByYear.ToLower() switch
                {
                    "asc" => query.OrderBy(a => a.ReleaseYear),
                    "desc" => query.OrderByDescending(a => a.ReleaseYear),
                    _ => query
                };
            }

            var result = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(a => new AlbumDto
                {
                    AlbumId = a.AlbumId,
                    AlbumName = a.AlbumName,
                    ReleaseYear = a.ReleaseYear,
                    SingerId = a.SingerId,
                    AlbumURL = a.AlbumURL,
                    ReleaseCountryId = a.ReleaseCountryId,
                    LableId = a.LableId,
                    GenreId = a.GenreId,
                    StyleId = a.StyleId
                })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsByGenresAndStyles(List<int>? genreIds, List<int>? styleIds)
        {
            var query = _context.Album.AsQueryable();

            if (genreIds != null && genreIds.Any())
                query = query.Where(a => a.GenreId.HasValue && genreIds.Contains(a.GenreId.Value));

            if (styleIds != null && styleIds.Any())
                query = query.Where(a => a.StyleId.HasValue && styleIds.Contains(a.StyleId.Value));

            return await query.Select(a => new AlbumDto
            {
                AlbumId = a.AlbumId,
                AlbumName = a.AlbumName,
                ReleaseYear = a.ReleaseYear,
                AlbumURL = a.AlbumURL,
                SingerId = a.SingerId,
                ReleaseCountryId = a.ReleaseCountryId,
                LableId = a.LableId,
                GenreId = a.GenreId,
                StyleId = a.StyleId
            }).ToListAsync();
        }


        public async Task<AlbumDto?> GetAlbumById(int id)
        {
            var a = await _context.Album.FindAsync(id);
            return a == null ? null : new AlbumDto
            {
                AlbumId = a.AlbumId,
                AlbumName = a.AlbumName,
                ReleaseYear = a.ReleaseYear,
                SingerId = a.SingerId,
                AlbumURL = a.AlbumURL,
                ReleaseCountryId = a.ReleaseCountryId,
                LableId = a.LableId,
                GenreId = a.GenreId,
                StyleId = a.StyleId
            };
        }

        public async Task<AlbumDto> CreateAlbum(AlbumDto album)
        {
            var newAlbum = new Album
            {
                AlbumName = album.AlbumName,
                ReleaseYear = album.ReleaseYear,
                SingerId = album.SingerId,
                AlbumURL = album.AlbumURL,
                ReleaseCountryId = album.ReleaseCountryId,
                LableId = album.LableId,
                GenreId = album.GenreId,
                StyleId = album.StyleId
            };

            _context.Album.Add(newAlbum);
            await _context.SaveChangesAsync();

            album.AlbumId = newAlbum.AlbumId;
            return album;
        }

        public async Task<bool> UpdateAlbum(int id, AlbumDto album)
        {
            var a = await _context.Album.FindAsync(id);
            if (a == null) return false;

            a.AlbumName = album.AlbumName;
            a.ReleaseYear = album.ReleaseYear;
            a.SingerId = album.SingerId;
            a.AlbumURL = album.AlbumURL;
            a.ReleaseCountryId = album.ReleaseCountryId;
            a.LableId = album.LableId;
            a.GenreId = album.GenreId;
            a.StyleId = album.StyleId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAlbum(int id)
        {
            var a = await _context.Album.FindAsync(id);
            if (a == null) return false;

            _context.Album.Remove(a);
            await _context.SaveChangesAsync();
            return true;
        }


    }
}
