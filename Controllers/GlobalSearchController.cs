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
            bool exactMatch = query.Length < 4;

            var tracks = await _context.Track
                .Where(t => exactMatch
                    ? t.TrackName.ToLower() == query
                    : t.TrackName.ToLower().Contains(query))
                .Select(t => new
                {
                    Id = t.TrackId,
                    Name = t.TrackName,
                    Type = "Track"
                })
                .ToListAsync();

            var singers = await _context.Singer
                .Where(s => exactMatch
                    ? s.SingerFullName.ToLower() == query
                    : s.SingerFullName.ToLower().Contains(query))
                .Select(s => new
                {
                    Id = s.SingerId,
                    Name = s.SingerFullName,
                    Type = "Singer"
                })
                .ToListAsync();

            var labels = await _context.Lable
                .Where(l => exactMatch
                    ? l.LableName.ToLower() == query
                    : l.LableName.ToLower().Contains(query))
                .Select(l => new
                {
                    Id = l.LableId,
                    Name = l.LableName,
                    Type = "Label"
                })
                .ToListAsync();

            var albums = await _context.Album
                .Where(a => exactMatch
                    ? a.AlbumName.ToLower() == query
                    : a.AlbumName.ToLower().Contains(query))
                .Select(a => new
                {
                    Id = a.AlbumId,
                    Name = a.AlbumName,
                    Type = "Album"
                })
                .ToListAsync();

            var result = tracks
                .Concat(singers)
                .Concat(labels)
                .Concat(albums);

            return Ok(result);
        }

    }
}
