using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models.DTO
{
    public class RegisterDTO
    {
        [Required]
        public required string Username { get; set; }
        [Required]
        public required string Password { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string SteamId { get; set; }
    }
}
