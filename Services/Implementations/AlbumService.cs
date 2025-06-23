using VinylBack.DTOs;
using VinylBack.Context;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<AlbumDto>> GetAllAlbums(int page, int limit)
        {
            return await _context.Album
                .Include(a => a.Singer)
                .Include(a => a.ReleaseCountry)
                .Include(a => a.Lable)
                .Include(a => a.Genre)
                .Include(a => a.Style)
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
