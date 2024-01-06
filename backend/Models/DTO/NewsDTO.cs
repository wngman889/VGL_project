using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models.DTO
{
    public class NewsDTO
    {
        [Required]
        public required string SteamId { get; set; }

        [Required]
        public required int MaxCount { get; set; }
    }
}
