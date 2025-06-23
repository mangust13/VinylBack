using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrackInBasketController : ControllerBase
    {
        private readonly ITrackInBasketService _service;

        public TrackInBasketController(ITrackInBasketService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<TrackInBasketDTO>>> GetAll()
        {
            return Ok(await _service.GetAllTrackInBasketServices());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<TrackInBasketDTO>> GetById(int id)
        {
            var item = await _service.GetTrackInBasketServiceById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<TrackInBasketDTO>> Create([FromBody] TrackInBasketDTO dto)
        {
            var created = await _service.CreateTrackInBasketService(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.TrackInBasketId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TrackInBasketDTO dto)
        {
            var updated = await _service.UpdateTrackInBasketService(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteTrackInBasketService(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
