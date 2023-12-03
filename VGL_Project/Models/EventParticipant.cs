using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VGL_Project.Models
{
    public class EventParticipant
    {
        [Key]
        public int Id { get; set; }

        public int ParticipantId { get; set; }
        public required User Participant { get; set; }

        public int EventId { get; set; }
        public required Event Event { get; set; }
    }
}
