using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _service;

        public BasketController(IBasketService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BasketDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            return Ok(await _service.GetAllBaskets(page, limit));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> GetById(int id)
        {
            var item = await _service.GetBasketById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Create([FromBody] BasketDTO dto)
        {
            var created = await _service.CreateBasket(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BasketId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BasketDTO dto)
        {
            var updated = await _service.UpdateBasket(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteBasket(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
