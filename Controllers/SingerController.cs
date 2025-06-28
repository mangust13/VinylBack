using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SingerController : ControllerBase
    {
        private readonly ISingerService _singerService;
        public SingerController(ISingerService singerService)
        {
            _singerService = singerService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<SingerDto>>> GetAll(
     [FromQuery] int page = 1,
     [FromQuery] int limit = 10,
     [FromQuery] List<int>? genreIds = null,
     [FromQuery] List<int>? styleIds = null,
     [FromQuery] string? sortByName = null)
        {
            var singers = await _singerService.GetAllSingers(
                page, limit, genreIds, styleIds, sortByName);
            return Ok(singers);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<SingerDto>> GetById(int id)
        {
            var singer = await _singerService.GetSingerById(id);
            return singer == null ? NotFound() : Ok(singer);
        }

        [HttpPost]
        public async Task<ActionResult<SingerDto>> Create([FromBody] SingerDto singer)
        {
            var newSinger = await _singerService.CreateSinger(singer);
            return CreatedAtAction(nameof(GetById), new { id = newSinger.SingerId }, newSinger);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SingerDto singer)
        {
            var updated = await _singerService.UpdateSinger(id, singer);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _singerService.DeleteSinger(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
