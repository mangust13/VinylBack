using System.ComponentModel.DataAnnotations;

namespace VinylBack.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required, MaxLength(100)]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; } = "User";

    }
}
