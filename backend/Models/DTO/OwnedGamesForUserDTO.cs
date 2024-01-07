using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models.DTO
{
    public class OwnedGamesForUserDTO
    {
        [Required]
        public required string SteamId { get; set; }

        public bool IncludeAppInfo { get; set; } = false;

        public bool IncludeFreeGames { get; set; } = false;
    }
}
