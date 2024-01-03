using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models
{
    public class ReviewRecommendation
    {
        [Key]
        public int Id { get; set; }

        [Column("AuthorId")]
        public int AuthorId { get; set; }
        public required User Author { get; set; }

        [Column("GameId")]
        public int GameId { get; set; }
        public required Game Game { get; set; }

        [Column("ReviewTitle")]
        [MaxLength(255)]
        public required string ReviewTitle { get; set; }

        public int Rating { get; set; }

        [Column("ReviewDescription")]
        [MaxLength(255)]
        public required string ReviewDescription { get; set; }
    }
}
