using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AlbumsController : Controller
    {
        private readonly IAlbumService _albumService;
        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<AlbumDto>> GetAll()
        {
            return Ok(await _albumService.GetAllAlbumsAsync());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AlbumDto>>> GetById(int id)
        {
            var album = await _albumService.GetAlbumByIdAsync(id);
            return album == null ? NotFound() : Ok(album);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<AlbumDto>> Create([FromBody] AlbumDto album)
        {
            var newAlbum = await _albumService.CreateAlbumAsync(album);
            return CreatedAtAction(nameof(GetById), new {id = newAlbum.AlbumId}, newAlbum);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(int id, [FromBody] AlbumDto album)
        {
            var updated = await _albumService.UpdateAlbumAsync(id, album);
            return updated ? NoContent() : NotFound();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            var deleted = await _albumService.DeleteAlbumAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
