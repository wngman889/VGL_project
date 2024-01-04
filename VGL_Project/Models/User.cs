using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Username { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Password { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Email { get; set; }

        [Column("CreatedAt")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Column("steam_id")]
        [MaxLength(255)]
        public required string? SteamId { get; set; }


        public List<UserGame> OwnedGames { get; set; }

        public List<ReviewRecommendation> AuthoredReviews { get; set; }

        public List<Event> AuthoredEvents { get; set; }

        public List<EventParticipant> ParticipatedEvents { get; set; }
    }
}
