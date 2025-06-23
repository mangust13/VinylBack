using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ITrackService
    {
        Task<IEnumerable<TrackDto>> GetAllTracks(int page, int limit);
        Task<TrackDto?> GetTrackById(int id);
        Task<TrackDto?> CreateTrack(TrackDto track);
        Task<bool> UpdateTrack(int id, TrackDto track);
        Task<bool> DeleteTrack(int id);
    }
}
