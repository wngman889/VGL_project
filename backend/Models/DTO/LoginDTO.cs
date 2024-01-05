using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models.DTO
{
    public class LoginDTO
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
