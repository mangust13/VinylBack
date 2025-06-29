using Microsoft.AspNetCore.Mvc;
using VinylBack.Services;
using VinylBack.DTOs;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FilteredContentController : ControllerBase
    {
        private readonly IAlbumService _albumService;
        private readonly ISingerService _singerService;
        private readonly ITrackService _trackService;

        public FilteredContentController(
            IAlbumService albumService,
            ISingerService singerService,
            ITrackService trackService)
        {
            _albumService = albumService;
            _singerService = singerService;
            _trackService = trackService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFilteredContent(
            [FromQuery] List<int>? genreIds,
            [FromQuery] List<int>? styleIds)
        {
            var albums = await _albumService.GetAlbumsByGenresAndStyles(genreIds, styleIds);
            var singers = await _singerService.GetSingersByGenresAndStyles(genreIds, styleIds);
            var tracks = await _trackService.GetTracksByGenresAndStyles(genreIds, styleIds);

            return Ok(new
            {
                albums,
                singers,
                tracks
            });
        }
    }
}


