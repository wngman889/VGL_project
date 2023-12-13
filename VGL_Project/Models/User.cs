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

        [Column("profile_desc")]
        [MaxLength(255)]
        public required string ProfileDesc { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<UserGame> OwnedGames { get; set; }

        public List<ReviewRecommendation> AuthoredReviews { get; set; }

        public List<Event> AuthoredEvents { get; set; }

        public List<EventParticipant> ParticipatedEvents { get; set; }
    }
}
