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
        public async Task<ActionResult<PagedResultDto<TrackDto>>> GetAll(
     [FromQuery] int page = 1,
     [FromQuery] int limit = 10,
     [FromQuery] List<int>? genreIds = null,
     [FromQuery] List<int>? styleIds = null,
     [FromQuery] double? minPrice = null,
     [FromQuery] double? maxPrice = null,
     [FromQuery] string? sortByDuration = null)
        {
            var tracks = await _trackService.GetAllTracks(page, limit, genreIds, styleIds, minPrice, maxPrice, sortByDuration);
            return Ok(tracks);
        }


        [HttpGet("{id}")]
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
