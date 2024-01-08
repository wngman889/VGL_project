using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Column("AuthorId")]
        public int AuthorId { get; set; }
        public required User Author { get; set; }

        [Column("GameId")]
        public int GameId { get; set; }
        public required Game Game { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(255)]
        public required string Description { get; set; }

        [Column("ImageEvent")]
        public byte[] EventImage { get; set; }

        [NotMapped] // This property is not stored in the database
        public string EventImageBase64 => EventImage != null ? Convert.ToBase64String(EventImage) : null;

        public List<EventParticipant>? Participants { get; set; }
    }
}
