using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using VinylBack.Context;
using VinylBack.DTOs;
using VinylBack.Models;
using Npgsql;


namespace VinylBack.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly VinylContext _context;

        public AuthController(VinylContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (await _context.AppUser.AnyAsync(u => u.Email == dto.Email))
                return BadRequest("Користувач з такою поштою вже існує.");

            using var hmac = new HMACSHA512();

            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));
            var salt = Convert.ToBase64String(hmac.Key);

            var sql = @"
                INSERT INTO ""AppUser"" (""Email"", ""PasswordHash"", ""PasswordSalt"", ""RoleId"", ""UserFullName"")
                VALUES (@p0, @p1, @p2, @p3, @p4);
            ";

            try
            {
                await _context.Database.ExecuteSqlRawAsync(sql, dto.Email, hash, salt, 1, dto.UserFullName ?? "");
            }
            catch (Exception ex)
            {
                return BadRequest("DB error: " + ex.Message);
            }

            return Ok("Користувача успішно зареєстровано.");
        }

        //[HttpPost("login")]
        //public async Task<ActionResult<string>> Login(LoginDto dto)
        //{
        //    var user = await _context.AppUser
        //        .Include(u => u.Role)
        //        .SingleOrDefaultAsync(u => u.Email == dto.Email);

        //    if (user == null)
        //        return Unauthorized("Invalid username");

        //    using var hmac = new HMACSHA512(user.PasswordSalt);
        //    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

        //    if (!computedHash.SequenceEqual(user.PasswordHash))
        //        return Unauthorized("Invalid password");

        //    var createdUser = await _context.AppUser
        //        .Include(u => u.Role)
        //        .FirstAsync(u => u.UserId == user.UserId);



        //    return Ok();
        //}

    }
}
