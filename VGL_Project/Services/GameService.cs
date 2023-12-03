namespace VGL_Project.Services
{
    using Microsoft.EntityFrameworkCore;
    using VGL_Project.Data;
    using VGL_Project.Models;
    using VGL_Project.Models.Interfaces;

    public class GameService : IGameService
    {
        private readonly VGLDbContext _dbContext;

        private readonly ILogger<GameService> _logger;

        public GameService(VGLDbContext dbContext, ILogger<GameService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new game to the database.
        /// </summary>
        /// <param name="title">The title of the game.</param>
        /// <param name="gameDesc">The description of the game.</param>
        /// <param name="genre">The genre of the game.</param>
        /// <returns>A Task representing the asynchronous operation.</returns>
        /// <remarks>
        /// This method creates a new Game instance with the provided title, game description, and genre.
        /// It then adds the new game to the DbContext and saves changes to the database asynchronously.
        /// If an exception occurs during the process, it is logged using the ILogger.
        /// </remarks>
        public async Task AddGame(string title, string gameDesc, string genre)
        {
            try
            {
                // Create a new Game instance
                var newGame = new Game
                {
                    Title = title,
                    GameDesc = gameDesc,
                    Genre = genre
                };

                // Add the new game to the DbContext
                _dbContext.Games.Add(newGame);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }
    }

}
