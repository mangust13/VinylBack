using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VinylBack.DTOs;
using VinylBack.Services;

namespace VinylBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SingersController : ControllerBase
    {
        private readonly ISingerService _singerService;
        public SingersController(ISingerService singerService)
        {
            _singerService = singerService;
        }
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SingerDTO>>> GetAll()
        {
            var singers = await _singerService.GetAllSingersAsync();
            return Ok(singers);
        }
    }
}
