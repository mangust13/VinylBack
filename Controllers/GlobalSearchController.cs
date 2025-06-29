using Microsoft.AspNetCore.Mvc;
using VinylBack.Context;
using Microsoft.EntityFrameworkCore;

namespace VinylBack.Controllers
{
        [ApiController]
        [Route("api/[controller]")]
        public class GlobalSearchController : ControllerBase
        {
            private readonly VinylContext _context;

            public GlobalSearchController(VinylContext context)
            {
                _context = context;
            }

        [HttpGet]
        public async Task<IActionResult> Search([FromQuery] string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return BadRequest("Query cannot be empty.");

            query = query.Trim().ToLower();

            var tracks = await _context.Track
                .Where(t => t.TrackName.ToLower().Contains(query))
                .Select(t => new
                {
                    Id = t.TrackId,
                    Name = t.TrackName
                })
                .ToListAsync();

            var singers = await _context.Singer
                .Where(s => s.SingerFullName.ToLower().Contains(query))
                .Select(s => new
                {
                    Id = s.SingerId,
                    Name = s.SingerFullName
                })
                .ToListAsync();

            var albums = await _context.Album
                .Where(a => a.AlbumName.ToLower().Contains(query))
                .Select(a => new
                {
                    Id = a.AlbumId,
                    Name = a.AlbumName
                })
                .ToListAsync();

            return Ok(new
            {
                albums,
                singers,
                tracks
            });
        }
    }
}
