using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class StyleService : IStyleService
    {
        private readonly VinylContext _context;

        public StyleService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StyleDto>> GetAllStyles(int page, int limit)
        {
            return await _context.Style
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(s => new StyleDto
                {
                    StyleId = s.StyleId,
                    StyleName = s.StyleName
                })
                .ToListAsync();
        }

        public async Task<StyleDto?> GetStyleById(int id)
        {
            var style = await _context.Style.FindAsync(id);
            if (style == null) return null;

            return new StyleDto
            {
                StyleId = style.StyleId,
                StyleName = style.StyleName
            };
        }

        public async Task<StyleDto> CreateStyle(StyleDto styleDto)
        {
            var style = new Style { StyleName = styleDto.StyleName };
            _context.Style.Add(style);
            await _context.SaveChangesAsync();

            styleDto.StyleId = style.StyleId;
            return styleDto;
        }

        public async Task<StyleDto?> UpdateStyle(int id, StyleDto styleDto)
        {
            var style = await _context.Style.FindAsync(id);
            if (style == null) return null;

            style.StyleName = styleDto.StyleName;
            await _context.SaveChangesAsync();

            return styleDto;
        }

        public async Task<bool> DeleteStyle(int id)
        {
            var style = await _context.Style.FindAsync(id);
            if (style == null) return false;

            _context.Style.Remove(style);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
