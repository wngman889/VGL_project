using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public required string Title { get; set; }

        [MaxLength(255)]
        public required string GameDesc { get; set; }

        [MaxLength(255)]
        public required string Genre { get; set; }

        public List<UserGame> Owners { get; set; }

        public List<ReviewRecommendation> Reviews { get; set; }

        public List<Event> Events { get; set; }
    }
}
