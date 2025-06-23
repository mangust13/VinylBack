using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class TrackService : ITrackService
    {
        private readonly VinylContext _context;
        public TrackService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TrackDto>> GetAllTracks(
            int page, 
            int limit,
            List<int>? genreIds = null,
            List<int>? styleIds = null,
            double? minPrice = null,
            double? maxPrice = null,
            string? sortByDuration = null)
        {
            var query = _context.Track
                .Include(t => t.Album)
                .AsQueryable();

            if (genreIds != null && genreIds.Any())
            {
                query = query.Where(t =>
                    t.Album != null &&
                    t.Album.GenreId.HasValue &&
                    genreIds.Contains(t.Album.GenreId.Value));
            }

            if (styleIds != null && styleIds.Any())
            {
                query = query.Where(t =>
                    t.Album != null &&
                    t.Album.StyleId.HasValue &&
                    styleIds.Contains(t.Album.StyleId.Value));
            }

            if (minPrice.HasValue)
                query = query.Where(t => t.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(t => t.Price <= maxPrice.Value);

            if (!string.IsNullOrEmpty(sortByDuration))
            {
                query = sortByDuration.ToLower() switch
                {
                    "asc" => query.OrderBy(t => t.TrackDuration),
                    "desc" => query.OrderByDescending(t => t.TrackDuration),
                    _ => query
                };
            }

            var result = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(t => new TrackDto
                {
                    TrackId = t.TrackId,
                    TrackName = t.TrackName,
                    TrackDuration = t.TrackDuration,
                    Price = t.Price,
                    AlbumId = t.AlbumId,
                    TrackURL = t.TrackURL
                })
                .ToListAsync();

            return result;
        }

        public async Task<IEnumerable<TrackDto>> GetTracksByGenresAndStyles(List<int>? genreIds, List<int>? styleIds)
        {
            var query = _context.Track
                .Include(t => t.Album)
                .AsQueryable();

            if (genreIds != null && genreIds.Any())
                query = query.Where(t => t.Album != null && t.Album.GenreId.HasValue && genreIds.Contains(t.Album.GenreId.Value));

            if (styleIds != null && styleIds.Any())
                query = query.Where(t => t.Album != null && t.Album.StyleId.HasValue && styleIds.Contains(t.Album.StyleId.Value));

            return await query.Select(t => new TrackDto
            {
                TrackId = t.TrackId,
                TrackName = t.TrackName,
                TrackDuration = t.TrackDuration,
                Price = t.Price,
                TrackURL = t.TrackURL,
                AlbumId = t.AlbumId
            }).ToListAsync();
        }

        public async Task<TrackDto?> GetTrackById(int id)
        {
            var t = await _context.Track.FindAsync(id);
            return t == null ? null : new TrackDto
            {
                TrackId = t.TrackId,
                TrackName = t.TrackName,
                TrackDuration = t.TrackDuration,
                Price = t.Price,
                AlbumId = t.AlbumId,
                TrackURL = t.TrackURL
            };
        }

        public async Task<TrackDto?> CreateTrack(TrackDto track)
        {
            var newTrack = new Track
            {
                TrackName = track.TrackName,
                TrackDuration = track.TrackDuration,
                Price = track.Price,
                AlbumId = track.AlbumId,
                TrackURL = track.TrackURL
            };

            _context.Track.Add(newTrack);
            await _context.SaveChangesAsync();

            track.TrackId = newTrack.TrackId;
            return track;
        }

        public async Task<bool> UpdateTrack(int id, TrackDto track)
        {
            var updatedTrack = await _context.Track.FindAsync(id);
            if (updatedTrack == null) return false;

            updatedTrack.TrackName = track.TrackName;
            updatedTrack.TrackDuration = track.TrackDuration;
            updatedTrack.Price = track.Price;
            updatedTrack.AlbumId = track.AlbumId;
            updatedTrack.TrackURL = track.TrackURL;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTrack(int id)
        {
            var track = await _context.Track.FindAsync(id);
            if (track == null) return false;

            _context.Track.Remove(track);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
