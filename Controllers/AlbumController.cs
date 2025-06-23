using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumController : Controller
    {
        private readonly IAlbumService _albumService;
        public AlbumController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        public async Task<ActionResult<AlbumDto>> GetAll(
            [FromQuery] int page = 1, 
            [FromQuery] int limit = 10,
            [FromQuery] List<int>? genreIds = null,
            [FromQuery] List<int>? styleIds = null,
            [FromQuery] List<int>? lableIds = null,
            [FromQuery] List<int>? countryIds = null,
            [FromQuery] int? minYear = null,
            [FromQuery] int? maxYear = null,
            [FromQuery] string? sortByYear = null)
        {
            var albums = await _albumService.GetAllAlbums(
                page, limit, genreIds, styleIds, lableIds, countryIds, minYear, maxYear, sortByYear);
            return Ok(albums);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetById(int id)
        {
            var album = await _albumService.GetAlbumById(id);
            return album == null ? NotFound() : Ok(album);
        }

        [HttpPost]
        public async Task<ActionResult<AlbumDto>> Create([FromBody] AlbumDto album)
        {
            var newAlbum = await _albumService.CreateAlbum(album);
            return CreatedAtAction(nameof(GetById), new {id = newAlbum.AlbumId}, newAlbum);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AlbumDto album)
        {
            var updated = await _albumService.UpdateAlbum(id, album);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var deleted = await _albumService.DeleteAlbum(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
