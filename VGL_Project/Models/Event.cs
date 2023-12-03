using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public int AuthorId { get; set; }
        public required User Author { get; set; }

        public int GameId { get; set; }
        public required Game Game { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(255)]
        public required string Description { get; set; }

        public List<EventParticipant> Participants { get; set; }
    }
}
