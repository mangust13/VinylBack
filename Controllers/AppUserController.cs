using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserService _service;

        public AppUserController(IAppUserService service)
        {
            _service = service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUserDTO>>> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10)
        {
            return Ok(await _service.GetAllUsers(page, limit));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<AppUserDTO>> GetById(int id)
        {
            var user = await _service.GetUserById(id);
            return user == null ? NotFound() : Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<AppUserDTO>> Create([FromBody] AppUserDTO dto)
        {
            var created = await _service.CreateUser(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.UserId }, created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppUserDTO dto)
        {
            var updated = await _service.UpdateUser(id, dto);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteUser(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
