using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class LableService : ILableService
    {
        private readonly VinylContext _context;

        public LableService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LableDto>> GetAllLables(int page, int limit)
        {
            return await _context.Lable
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(l => new LableDto
                {
                    LableId = l.LableId,
                    LableName = l.LableName
                })
                .ToListAsync();
        }

        public async Task<LableDto?> GetByIdLable(int id)
        {
            var lable = await _context.Lable.FindAsync(id);
            if (lable == null) return null;

            return new LableDto
            {
                LableId = lable.LableId,
                LableName = lable.LableName
            };
        }

        public async Task<LableDto> CreateLable(LableDto lableDto)
        {
            var lable = new Lable { LableName = lableDto.LableName };
            _context.Lable.Add(lable);
            await _context.SaveChangesAsync();

            lableDto.LableId = lable.LableId;
            return lableDto;
        }

        public async Task<LableDto?> UpdateLable(int id, LableDto lableDto)
        {
            var lable = await _context.Lable.FindAsync(id);
            if (lable == null) return null;

            lable.LableName = lableDto.LableName;
            await _context.SaveChangesAsync();

            return lableDto;
        }

        public async Task<bool> DeleteLable(int id)
        {
            var lable = await _context.Lable.FindAsync(id);
            if (lable == null) return false;

            _context.Lable.Remove(lable);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
