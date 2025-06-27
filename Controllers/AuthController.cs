using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Security.Cryptography;
using System.Text;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;
using VinylBack.Services.Implementations;


namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly VinylContext _context;
        private readonly IConfiguration _configuration;
        public AuthController(VinylContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto, [FromServices] TokenService jwtService)
        {
            if (await _context.AppUser.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Користувач з таким email вже існує");

            using var hmac = new HMACSHA512();
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));
            var salt = Convert.ToBase64String(hmac.Key);

            var user = new AppUser
            {
                Email = dto.Email,
                PasswordHash = hash,
                PasswordSalt = salt,
                RoleId = 1,
                UserFullName = dto.UserFullName ?? "",
                PhoneNumber = null
            };

            _context.AppUser.Add(user);
            await _context.SaveChangesAsync();

            await _context.Entry(user).Reference(u => u.Role).LoadAsync();

            var token = jwtService.CreateToken(user);

            var userDto = new AppUserDTO
            {
                UserId = user.UserId,
                UserFullName = user.UserFullName,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                PasswordSalt = user.PasswordSalt,
                RoleId = user.RoleId
            };

            return Ok(new { token, user = userDto });
        }



        [HttpPost("login")]
        public async Task<ActionResult<object>> Login(LoginDto dto, [FromServices] TokenService jwtService)
        {
            var user = await _context.AppUser
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == dto.Email);

            if (user == null)
                return Unauthorized("Invalid email");

            using var hmac = new HMACSHA512(Convert.FromBase64String(user.PasswordSalt));
            var computedHash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));

            if (computedHash != user.PasswordHash)
                return Unauthorized("Invalid password");

            var token = jwtService.CreateToken(user);

            return Ok(new
            {
                token,
                user = new AppUserDTO
                {
                    UserId = user.UserId,
                    UserFullName = user.UserFullName,
                    PhoneNumber = user.PhoneNumber ?? "",
                    Email = user.Email,
                    PasswordHash = user.PasswordHash,
                    PasswordSalt = user.PasswordSalt,
                    RoleId = user.RoleId
                }
            });
        }



    }
}
