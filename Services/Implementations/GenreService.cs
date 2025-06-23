using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;

namespace VinylBack.Services
{
    public class GenreService : IGenreService
    {
        private readonly VinylContext _context;

        public GenreService(VinylContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GenreDto>> GetAllGenres(int page, int limit)
        {
            return await _context.Genre
                .Skip((page - 1) * limit)
                .Take(limit)
                .Select(g => new GenreDto
                {
                    GenreId = g.GenreId,
                    GenreName = g.GenreName
                })
                .ToListAsync();
        }

        public async Task<GenreDto?> GetGenreById(int id)
        {
            var genre = await _context.Genre.FindAsync(id);
            if (genre == null) return null;

            return new GenreDto
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
            };
        }

        public async Task<GenreDto> CreateGenre(GenreDto genreDto)
        {
            var genre = new Genre { GenreName = genreDto.GenreName };
            _context.Genre.Add(genre);
            await _context.SaveChangesAsync();

            genreDto.GenreId = genre.GenreId;
            return genreDto;
        }

        public async Task<GenreDto?> UpdateGenre(int id, GenreDto genreDto)
        {
            var genre = await _context.Genre.FindAsync(id);
            if (genre == null) return null;

            genre.GenreName = genreDto.GenreName;
            await _context.SaveChangesAsync();

            return genreDto;
        }

        public async Task<bool> DeleteGenre(int id)
        {
            var genre = await _context.Genre.FindAsync(id);
            if (genre == null) return false;

            _context.Genre.Remove(genre);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
