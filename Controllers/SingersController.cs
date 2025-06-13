using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SingersController : ControllerBase
    {
        private readonly ISingerService _singerService;
        public SingersController(ISingerService singerService)
        {
            _singerService = singerService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SingerDto>>> GetAll()
        {
            var singers = await _singerService.GetAllSingersAsync();
            return Ok(singers);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<SingerDto>> GetById(int id)
        {
            var singer = await _singerService.GetSingerByIdAsync(id);
            return singer == null ? NotFound() : Ok(singer);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<SingerDto>> Create([FromBody] SingerDto singer)
        {
            var newSinger = await _singerService.CreateSingerAsync(singer);
            return CreatedAtAction(nameof(GetById), new { id = newSinger.SingerId }, newSinger);
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SingerDto singer)
        {
            var updated = await _singerService.UpdateSingerAsync(id, singer);
            return updated ? NoContent() : NotFound();
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _singerService.DeleteSingerAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
