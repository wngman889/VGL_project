using Microsoft.EntityFrameworkCore;
using VGL_Project.Models;
using VGL_Project.Models.Enums;

namespace VGL_Project.Data
{
    public class VGLDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<UserGame> UserGames { get; set; }
        public DbSet<ReviewRecommendation> ReviewRecommendations { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventParticipant> EventParticipants { get; set; }


        private readonly IConfiguration _configuration;
        public VGLDbContext(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("VGLDb");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //GameStatus enum conversion rule
            modelBuilder
                .Entity<UserGame>()
                .Property(g => g.Status)
                .HasConversion(
                    v => v.ToString(),
                v => (GameStatus)Enum.Parse(typeof(GameStatus), v));
        }
    }
}
