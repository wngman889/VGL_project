using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models
{
    public class ReviewRecommendation
    {
        [Key]
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public required User Author { get; set; }

        public int GameId { get; set; }
        public required Game Game { get; set; }

        [MaxLength(255)]
        public required string ReviewTitle { get; set; }

        public int Rating { get; set; }

        [MaxLength(255)]
        public required string ReviewDescription { get; set; }
    }
}
