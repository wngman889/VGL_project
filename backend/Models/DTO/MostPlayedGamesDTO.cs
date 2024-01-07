using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models.DTO
{
    public class MostPlayedGamesDTO
    {
        [Required]
        public required string SteamId { get; set; }
        [Required]
        public required int Count { get; set; }
        public MostPlayedGamesDTO(string steamId, int count)
        {
            this.SteamId = steamId; 
            this.Count = count;
        }
    }
}
