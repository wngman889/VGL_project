using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models.DTO
{
    public class MostPlayedGamesDTO
    {
        [Required]
        public required string SteamId { get; set; }
        [Required]
        public required int Count { get; set; }
    }
}
