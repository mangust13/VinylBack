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

        public async Task<IEnumerable<TrackDto>> GetAllTracksAsync()
        {
            return await _context.Track
                .Select(t => new TrackDto
                {
                    TrackId = t.TrackId,
                    TrackName = t.TrackName,
                    TrackDuration = t.TrackDuration,
                    Price = t.Price,
                    AlbumId = t.AlbumId,
                    TrackURL = t.TrackURL
                }).ToListAsync();
        }

        public async Task<TrackDto?> GetTrackByIdAsync(int id)
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

        public async Task<TrackDto?> CreateTrackAsync(TrackDto track)
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

        public async Task<bool> UpdateTrackAsync(int id, TrackDto track)
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

        public async Task<bool> DeleteTrackAsync(int id)
        {
            var track = await _context.Track.FindAsync(id);
            if (track == null) return false;

            _context.Track.Remove(track);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
