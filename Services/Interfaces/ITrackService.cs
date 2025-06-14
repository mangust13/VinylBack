using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ITrackService
    {
        Task<IEnumerable<TrackDto>> GetAllTracksAsync();
        Task<TrackDto?> GetTrackByIdAsync(int id);
        Task<TrackDto?> CreateTrackAsync(TrackDto track);
        Task<bool> UpdateTrackAsync(int id, TrackDto track);
        Task<bool> DeleteTrackAsync(int id);
    }
}
