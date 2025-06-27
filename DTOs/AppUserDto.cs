namespace VinylBack.DTOs
{
    public class AppUserDTO
    {
        public int UserId { get; set; }
        public string UserFullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public int RoleId { get; set; }
    }
}
