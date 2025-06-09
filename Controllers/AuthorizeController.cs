using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VinylBack.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ProtectedController : ControllerBase
    {
        [HttpGet("secret")]
        public IActionResult GetSecret()
        {
            var username = User.Identity?.Name ?? "Unknown";
            return Ok($"Welcome, {username}! This is a protected endpoint.");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult GetAdminData()
        {
            return Ok("Only admins can see this message.");
        }
    }
}
