using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;
using VinylBack.Services;
using Microsoft.EntityFrameworkCore;
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
        List<int>? singerIds = null,
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
            .Include(a => a.Tracks)
            .AsQueryable();

        if (genreIds != null && genreIds.Any())
            query = query.Where(a => a.GenreId.HasValue && genreIds.Contains(a.GenreId.Value));

        if (styleIds != null && styleIds.Any())
            query = query.Where(a => a.StyleId.HasValue && styleIds.Contains(a.StyleId.Value));

        if (lableIds != null && lableIds.Any())
            query = query.Where(a => a.LableId.HasValue && lableIds.Contains(a.LableId.Value));

        if (countryIds != null && countryIds.Any())
            query = query.Where(a => a.ReleaseCountryId.HasValue && countryIds.Contains(a.ReleaseCountryId.Value));

        if (singerIds != null && singerIds.Any())
            query = query.Where(a => a.SingerId.HasValue && singerIds.Contains(a.SingerId.Value));

        if (minYear.HasValue)
            query = query.Where(a => a.ReleaseYear >= minYear.Value);
        if (maxYear.HasValue)
            query = query.Where(a => a.ReleaseYear <= maxYear.Value);

        if (!string.IsNullOrEmpty(sortByYear))
            query = sortByYear.ToLower() switch
            {
                "asc" => query.OrderBy(a => a.ReleaseYear),
                "desc" => query.OrderByDescending(a => a.ReleaseYear),
                _ => query
            };

        return await query
            .Skip((page - 1) * limit)
            .Take(limit)
            .Select(a => MapToDto(a))
            .ToListAsync();
    }

    public async Task<IEnumerable<AlbumDto>> GetAlbumsByGenresAndStyles(List<int>? genreIds, List<int>? styleIds)
    {
        var query = _context.Album
            .Include(a => a.Singer)
            .Include(a => a.ReleaseCountry)
            .Include(a => a.Lable)
            .Include(a => a.Genre)
            .Include(a => a.Style)
            .Include(a => a.Tracks)
            .AsQueryable();

        if (genreIds != null && genreIds.Any())
            query = query.Where(a => a.GenreId.HasValue && genreIds.Contains(a.GenreId.Value));
        if (styleIds != null && styleIds.Any())
            query = query.Where(a => a.StyleId.HasValue && styleIds.Contains(a.StyleId.Value));

        return await query
            .Select(a => MapToDto(a))
            .ToListAsync();
    }

    public async Task<AlbumDto?> GetAlbumById(int id)
    {
        var a = await _context.Album
            .Include(a => a.Singer)
            .Include(a => a.ReleaseCountry)
            .Include(a => a.Lable)
            .Include(a => a.Genre)
            .Include(a => a.Style)
            .Include(a => a.Tracks)
            .FirstOrDefaultAsync(a => a.AlbumId == id);

        return a == null ? null : MapToDto(a);
    }

    public async Task<AlbumDto> CreateAlbum(AlbumDto album)
    {
        var newAlbum = new Album
        {
            AlbumName = album.AlbumName,
            ReleaseYear = album.ReleaseYear,
            AlbumURL = album.AlbumURL,
            SingerId = album.Singer?.SingerId,
            ReleaseCountryId = album.ReleaseCountry?.CountryId,
            LableId = album.Lable?.LableId,
            GenreId = album.Genre?.GenreId,
            StyleId = album.Style?.StyleId
        };

        _context.Album.Add(newAlbum);
        await _context.SaveChangesAsync();

        return await GetAlbumById(newAlbum.AlbumId) ?? throw new Exception("Failed to create album");
    }

    public async Task<bool> UpdateAlbum(int id, AlbumDto album)
    {
        var a = await _context.Album.FindAsync(id);
        if (a == null) return false;

        a.AlbumName = album.AlbumName;
        a.ReleaseYear = album.ReleaseYear;
        a.AlbumURL = album.AlbumURL;
        a.SingerId = album.Singer?.SingerId;
        a.ReleaseCountryId = album.ReleaseCountry?.CountryId;
        a.LableId = album.Lable?.LableId;
        a.GenreId = album.Genre?.GenreId;
        a.StyleId = album.Style?.StyleId;

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

    private static AlbumDto MapToDto(Album a)
    {
        return new AlbumDto
        {
            AlbumId = a.AlbumId,
            AlbumName = a.AlbumName,
            ReleaseYear = a.ReleaseYear,
            AlbumURL = a.AlbumURL,
            Singer = a.Singer == null ? null : new SingerDto
            {
                SingerId = a.Singer.SingerId,
                SingerFullName = a.Singer.SingerFullName,
                SingerURL = a.Singer.SingerURL
            },
            ReleaseCountry = a.ReleaseCountry == null ? null : new CountryDto
            {
                CountryId = a.ReleaseCountry.CountryId,
                CountryName = a.ReleaseCountry.CountryName
            },
            Lable = a.Lable == null ? null : new LableDto
            {
                LableId = a.Lable.LableId,
                LableName = a.Lable.LableName
            },
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
            Tracks = a.Tracks?.Select(t => new TrackDto
            {
                TrackId = t.TrackId,
                TrackName = t.TrackName,
                TrackDuration = t.TrackDuration,
                Price = t.Price,
                TrackURL = t.TrackURL,
                AlbumId = t.AlbumId,
            }).ToList() ?? new List<TrackDto>()
        };
    }
}
