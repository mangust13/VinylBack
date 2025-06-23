using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface ITrackService
    {
        Task<IEnumerable<TrackDto>> GetAllTracks(int page,
            int limit,
            List<int>? genreIds = null,
            List<int>? styleIds = null,
            double? minPrice = null,
            double? maxPrice = null,
            string? sortByDuration = null);
        Task<IEnumerable<TrackDto>> GetTracksByGenresAndStyles(List<int>? genreIds, List<int>? styleIds);
        Task<TrackDto?> GetTrackById(int id);
        Task<TrackDto?> CreateTrack(TrackDto track);
        Task<bool> UpdateTrack(int id, TrackDto track);
        Task<bool> DeleteTrack(int id);
    }
}
