using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackController : ControllerBase
    {
        private readonly ITrackService _trackService;

        public TrackController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            return Ok(await _trackService.GetAllTracks(page, limit));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TrackDto>> GetById(int id)
        {
            var track = await _trackService.GetTrackById(id);
            return track == null ? NotFound() : Ok(track);
        }

        [HttpPost]
        public async Task<ActionResult<TrackDto>> Create([FromBody] TrackDto track)
        {
            var created = await _trackService.CreateTrack(track);
            return CreatedAtAction(nameof(GetById), new { id = created.TrackId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrackDto track)
        {
            var updated = await _trackService.UpdateTrack(id, track);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _trackService.DeleteTrack(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
