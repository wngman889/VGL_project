using Microsoft.EntityFrameworkCore;
using VGL_Project.Data;
using VGL_Project.Models;
using VGL_Project.Models.Interfaces;

namespace VGL_Project.Services
{
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
        public async Task<bool> AddGame(string title, string gameDesc, string genre)
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

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Retrieves a game from the database based on the specified ID.
        /// </summary>
        /// <param name="id">The ID of the game to retrieve.</param>
        /// <returns>
        /// A Task representing the asynchronous operation. 
        /// The method returns a nullable Game object. 
        /// If the game with the specified ID is found, the object is returned; otherwise, it returns null.
        /// </returns>
        /// <remarks>
        /// This method searches for a game in the database using the provided ID asynchronously.
        /// If the game is found, it is returned; otherwise, null is returned.
        /// If an exception occurs during the process, it is logged using the ILogger.
        /// </remarks>
        public async Task<Game?> GetGame(int id)
        {
            try
            {
                var game = await _dbContext.Games.FindAsync(id);
                return game;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retrieves all games from the database.
        /// </summary>
        /// <returns>
        /// A Task representing the asynchronous operation. 
        /// The method returns a nullable IEnumerable of Game objects. 
        /// If games are found in the database, the collection is returned; otherwise, it returns null.
        /// </returns>
        /// <remarks>
        /// This method retrieves all games from the database asynchronously.
        /// If games are found, the collection is returned; otherwise, null is returned.
        /// If an exception occurs during the process, it is logged using the ILogger.
        /// </remarks>
        public async Task<IEnumerable<Game>?> GetGames()
        {
            try
            {
                var games = await _dbContext.Games.ToListAsync();
                return games;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<bool> UpdateGame(int id, string newTitle, string newGameDesc, string newGenre)
        {
            try
            {
                // Find the game in the database
                var existingGame = await _dbContext.Games.FindAsync(id);

                // If the game is not found, return false
                if (existingGame == null)
                    return false;

                // Update the properties of the existing game
                existingGame.Title = newTitle;
                existingGame.GameDesc = newGameDesc;
                existingGame.Genre = newGenre;

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteGame(int id)
        {
            try
            {
                // Find the game in the database
                var gameToDelete = await _dbContext.Games.FindAsync(id);

                // If the game is not found, return false
                if (gameToDelete == null)
                    return false;

                // Remove the game from the DbContext
                _dbContext.Games.Remove(gameToDelete);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

    }

}
