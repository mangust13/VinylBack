using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PurchasedTrackController : ControllerBase
    {
        private readonly IPurchasedTrackService _service;

        public PurchasedTrackController(IPurchasedTrackService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<PurchasedTrackDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            return Ok(await _service.GetAllPurchasedTracks(page, limit));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<PurchasedTrackDTO>> GetById(int id)
        {
            var item = await _service.GetPurchasedTrackById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<PurchasedTrackDTO>> Create([FromBody] PurchasedTrackDTO dto)
        {
            var created = await _service.CreatePurchasedTrack(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.PurchasedTrackId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PurchasedTrackDTO dto)
        {
            var updated = await _service.UpdatePurchasedTrack(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeletePurchasedTrack(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
