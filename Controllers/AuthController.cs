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
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            using var hmac = new HMACSHA512();
            var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)));
            var salt = Convert.ToBase64String(hmac.Key);

            var connStr = _configuration.GetConnectionString("DefaultConnection");

            await using var conn = new NpgsqlConnection(connStr);
            await conn.OpenAsync();

            var cmd = new NpgsqlCommand(@"
                INSERT INTO ""AppUser"" (""Email"", ""PasswordHash"", ""PasswordSalt"", ""RoleId"", ""UserFullName"")
                VALUES (@email, @hash, @salt, @roleId, @fullName);
            ", conn);

            cmd.Parameters.AddWithValue("email", dto.Email);
            cmd.Parameters.AddWithValue("hash", hash);
            cmd.Parameters.AddWithValue("salt", salt);
            cmd.Parameters.AddWithValue("roleId", 1);
            cmd.Parameters.AddWithValue("fullName", dto.UserFullName ?? "");

            try
            {
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                return BadRequest("DB error: " + ex.Message);
            }

            return Ok("Успішно додано");
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
