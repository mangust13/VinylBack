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

        public async Task<IEnumerable<AlbumDto>> GetAllAlbumsAsync()
        {
            return await _context.Album
                .Select(a => new AlbumDto
                {
                    AlbumId = a.AlbumId,
                    ReleaseYear = a.ReleaseYear,
                    ReleaseCountry = a.ReleaseCountry,
                    Lable = a.Lable,
                    Genre = a.Genre,
                    Style = a.Style,
                    SingerId = a.SingerId,
                    AlbumURL = a.AlbumURL
                }).ToListAsync();
        }
        public async Task<AlbumDto?> GetAlbumByIdAsync(int id)
        {
            var album = await _context.Album.FindAsync(id);
            return album == null ? null : new AlbumDto
            {
                AlbumId = album.AlbumId,
                ReleaseYear = album.ReleaseYear,
                ReleaseCountry = album.ReleaseCountry,
                Lable = album.Lable,
                Genre = album.Genre,
                Style = album.Style,
                SingerId = album.SingerId,
                AlbumURL = album.AlbumURL
            };

        }

        public async Task<AlbumDto> CreateAlbumAsync(AlbumDto album)
        {
            var newAlbum = new Album
            {
                ReleaseYear = album.ReleaseYear,
                ReleaseCountry = album.ReleaseCountry,
                Lable = album.Lable,
                Genre = album.Genre,
                Style = album.Style,
                SingerId = album.SingerId,
                AlbumURL = album.AlbumURL
            };

            _context.Album.Add(newAlbum);
            await _context.SaveChangesAsync();

            album.AlbumId = newAlbum.AlbumId;
            return album;
        }

        public async Task<bool> UpdateAlbumAsync(int id, AlbumDto album)
        {
            var updatedAlbum = await _context.Album.FindAsync(id);
            if (updatedAlbum == null)
                return false;

            updatedAlbum.ReleaseYear = album.ReleaseYear;
            updatedAlbum.ReleaseCountry = album.ReleaseCountry;
            updatedAlbum.Lable = album.Lable;
            updatedAlbum.Genre = album.Genre;
            updatedAlbum.Style = album.Style;
            updatedAlbum.SingerId = album.SingerId;
            updatedAlbum.AlbumURL = album.AlbumURL;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAlbumAsync(int id)
        {
            var album = await _context.Album.FindAsync(id);
            if (album == null)
                return false;

            _context.Album.Remove(album);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
