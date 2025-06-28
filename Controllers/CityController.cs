using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _service;

        public CityController(ICityService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<PagedResultDto<CityDTO>>> GetAll(
     [FromQuery] int page = 1,
     [FromQuery] int limit = 10)
        {
            var result = await _service.GetAllCities(page, limit);
            return Ok(result);
        }


        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CityDTO>> GetById(int id)
        {
            var city = await _service.GetCityById(id);
            return city == null ? NotFound() : Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult<CityDTO>> Create([FromBody] CityDTO dto)
        {
            var created = await _service.CreateCity(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CityId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CityDTO dto)
        {
            var updated = await _service.UpdateCity(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteCity(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
