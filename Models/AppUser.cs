using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VinylBack.Models
{
    public class AppUser
    {
        [Key]
        public int UserId { get; set; }

        public string UserFullName { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string? PhoneNumber { get; set; }

        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role Role { get; set; }

        
    }
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        [Required]
        public string RoleName { get; set; }
    }
}
