using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController : ControllerBase
    {
        private readonly IStyleService _service;

        public StyleController(IStyleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StyleDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            return Ok(await _service.GetAllStyles(page, limit));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StyleDto>> GetById(int id)
        {
            var style = await _service.GetStyleById(id);
            if (style == null) return NotFound();
            return Ok(style);
        }

        [HttpPost]
        public async Task<ActionResult<StyleDto>> Create([FromBody] StyleDto dto)
        {
            var created = await _service.CreateStyle(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.StyleId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] StyleDto dto)
        {
            var updated = await _service.UpdateStyle(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteStyle(id);
            return success ? NoContent() : NotFound();
        }
    }
}
