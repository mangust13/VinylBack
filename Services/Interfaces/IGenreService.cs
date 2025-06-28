using VinylBack.DTOs;

namespace VinylBack.Services
{
    public interface IGenreService
    {
        Task<PagedResultDto<GenreDto>> GetAllGenres(int page, int limit);

        Task<GenreDto?> GetGenreById(int id);
        Task<GenreDto> CreateGenre(GenreDto genreDto);
        Task<GenreDto?> UpdateGenre(int id, GenreDto genreDto);
        Task<bool> DeleteGenre(int id);
    }
}
