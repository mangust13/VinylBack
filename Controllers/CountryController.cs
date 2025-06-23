using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _service;

        public CountryController(ICountryService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CountryDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            return Ok(await _service.GetAllCountries(page, limit));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CountryDTO>> GetById(int id)
        {
            var country = await _service.GetCountryById(id);
            return country == null ? NotFound() : Ok(country);
        }

        [HttpPost]
        public async Task<ActionResult<CountryDTO>> Create([FromBody] CountryDTO dto)
        {
            var created = await _service.CreateCountry(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.CountryId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CountryDTO dto)
        {
            var updated = await _service.UpdateCountry(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteCountry(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
