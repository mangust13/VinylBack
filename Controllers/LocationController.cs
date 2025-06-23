using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<LocationDTO>>> GetAll()
        {
            return Ok(await _service.GetAllLocations());
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<LocationDTO>> GetById(int id)
        {
            var location = await _service.GetLocationById(id);
            return location == null ? NotFound() : Ok(location);
        }

        [HttpPost]
        public async Task<ActionResult<LocationDTO>> Create([FromBody] LocationDTO dto)
        {
            var created = await _service.CreateLocation(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.LocationId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LocationDTO dto)
        {
            var updated = await _service.UpdateLocation(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteLocation(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
