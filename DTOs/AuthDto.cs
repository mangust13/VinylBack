using System.ComponentModel.DataAnnotations;

namespace VinylBack.DTOs
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string UserFullName { get; set; }
    }

    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
