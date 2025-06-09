using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;
using VinylBack.Services;


namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly VinylContext _context;
        private readonly ITokenService _tokenService;

        public AuthController(VinylContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.users.AnyAsync(u => u.UserName == dto.UserName))
                return BadRequest("Username already exists");

            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            var token = _tokenService.CreateToken(user);

            return Ok(new { token });
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(LoginDto dto)
        {
            var user = await _context.users.SingleOrDefaultAsync(u => u.UserName == dto.UserName);
            if (user == null)
                return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
                return Unauthorized("Invalid password");

            var token = _tokenService.CreateToken(user);

            return Ok(new { token });
        }

    }
}
