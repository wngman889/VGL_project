using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using VGL_Project.Models.Enums;

namespace VGL_Project.Models
{
    public class UserGame
    {
        [Key]
        public int Id { get; set; }

        [Column("owner_id")]
        public int OwnerId { get; set; }
        public required User Owner { get; set; }

        [Column("game_id")]
        public int GameId { get; set; }
        public required Game Game { get; set; }

        public GameStatus Status { get; set; } = GameStatus.Owned;
    }
}
