using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LableController : ControllerBase
    {
        private readonly ILableService _service;

        public LableController(ILableService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResultDto<LableDto>>> GetAll(
    [FromQuery] int page = 1,
    [FromQuery] int limit = 10)
        {
            var result = await _service.GetAllLables(page, limit);
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<LableDto>> GetById(int id)
        {
            var lable = await _service.GetByIdLable(id);
            if (lable == null) return NotFound();
            return Ok(lable);
        }

        [HttpPost]
        public async Task<ActionResult<LableDto>> Create([FromBody] LableDto dto)
        {
            var created = await _service.CreateLable(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.LableId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LableDto dto)
        {
            var updated = await _service.UpdateLable(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _service.DeleteLable(id);
            return success ? NoContent() : NotFound();
        }
    }
}
