using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models
{
    public class ReviewRecommendation
    {
        [Key]
        public int Id { get; set; }

        [Column("author_id")]
        public int AuthorId { get; set; }
        public required User Author { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }
        public required Game Game { get; set; }

        [Column("review_title")]
        [MaxLength(255)]
        public required string ReviewTitle { get; set; }

        public int Rating { get; set; }

        [Column("review_description")]
        [MaxLength(255)]
        public required string ReviewDescription { get; set; }
    }
}
