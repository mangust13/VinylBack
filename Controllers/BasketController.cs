using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PagedResultDto<BasketDTO>>> GetAll(
    [FromQuery] int page = 1,
    [FromQuery] int limit = 10)
        {
            var result = await _service.GetAllBaskets(page, limit);
            return Ok(result);
        }


        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDTO>> GetById(int id)
        {
            var item = await _service.GetBasketById(id);
            return item == null ? NotFound() : Ok(item);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<BasketDTO>> Create([FromBody] BasketDTO dto)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            dto.UserId = int.Parse(userIdStr);
            var created = await _service.CreateBasket(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.BasketId }, created);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BasketDTO dto)
        {
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdStr)) return Unauthorized();

            dto.UserId = int.Parse(userIdStr);
            var updated = await _service.UpdateBasket(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteBasket(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
