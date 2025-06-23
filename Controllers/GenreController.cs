using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _service;

        public GenreController(IGenreService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GenreDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            return Ok(await _service.GetAllGenres(page, limit));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GenreDto>> GetById(int id)
        {
            var genre = await _service.GetGenreById(id);
            if (genre == null) return NotFound();
            return Ok(genre);
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> Create([FromBody] GenreDto dto)
        {
            var created = await _service.CreateGenre(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.GenreId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GenreDto dto)
        {
            var updated = await _service.UpdateGenre(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteGenre(id);
            return success ? NoContent() : NotFound();
        }
    }
}
