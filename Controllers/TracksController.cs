using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TracksController : ControllerBase
    {
        private readonly ITrackService _trackService;

        public TracksController(ITrackService trackService)
        {
            _trackService = trackService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TrackDto>>> GetAll()
        {
            return Ok(await _trackService.GetAllTracksAsync());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TrackDto>> GetById(int id)
        {
            var track = await _trackService.GetTrackByIdAsync(id);
            return track == null ? NotFound() : Ok(track);
        }

        [HttpPost]
        public async Task<ActionResult<TrackDto>> Create([FromBody] TrackDto track)
        {
            var created = await _trackService.CreateTrackAsync(track);
            return CreatedAtAction(nameof(GetById), new { id = created.TrackId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrackDto track)
        {
            var updated = await _trackService.UpdateTrackAsync(id, track);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _trackService.DeleteTrackAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
