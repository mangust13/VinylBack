using Microsoft.EntityFrameworkCore;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class PurchasedTrackService : IPurchasedTrackService
    {
        private readonly VinylContext _context;

        public PurchasedTrackService(VinylContext context)
        {
            _context = context;
        }

        public async Task<PagedResultDto<PurchasedTrackDTO>> GetAllPurchasedTracks(int page, int limit)
        {
            var query = _context.PurchasedTrack.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(p => new PurchasedTrackDTO
                {
                    PurchasedTrackId = p.PurchasedTrackId,
                    PurchaseId = p.PurchaseId,
                    TrackId = p.TrackId
                })
                .ToListAsync();

            return new PagedResultDto<PurchasedTrackDTO>
            {
                TotalCount = totalCount,
                Items = items
            };
        }


        public async Task<PurchasedTrackDTO?> GetPurchasedTrackById(int id)
        {
            var p = await _context.PurchasedTrack.FindAsync(id);
            return p == null ? null : new PurchasedTrackDTO
            {
                PurchasedTrackId = p.PurchasedTrackId,
                PurchaseId = p.PurchaseId,
                TrackId = p.TrackId
            };
        }

        public async Task<PurchasedTrackDTO> CreatePurchasedTrack(PurchasedTrackDTO dto)
        {
            var entity = new PurchasedTrack
            {
                PurchaseId = dto.PurchaseId,
                TrackId = dto.TrackId
            };

            _context.PurchasedTrack.Add(entity);
            await _context.SaveChangesAsync();

            dto.PurchasedTrackId = entity.PurchasedTrackId;
            return dto;
        }

        public async Task<bool> UpdatePurchasedTrack(int id, PurchasedTrackDTO dto)
        {
            var entity = await _context.PurchasedTrack.FindAsync(id);
            if (entity == null) return false;

            entity.PurchaseId = dto.PurchaseId;
            entity.TrackId = dto.TrackId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePurchasedTrack(int id)
        {
            var entity = await _context.PurchasedTrack.FindAsync(id);
            if (entity == null) return false;

            _context.PurchasedTrack.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
