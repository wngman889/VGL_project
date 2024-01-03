using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Owin.Security.Providers.OpenIDBase.Infrastructure;
using System.Data;
using VGL_Project.Data;
using VGL_Project.Models;
using VGL_Project.Models.Interfaces;

namespace VGL_Project.Services
{
    public class UserService : IUserService
    {
        private readonly VGLDbContext _dbContext;

        private readonly ILogger<UserService> _logger;

        public UserService(VGLDbContext dbContext, ILogger<UserService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user. Note: It is recommended to encrypt or hash the password for security.</param>
        /// <param name="email">The email of the user.</param>
        /// <param name="profileDesc">The profile description of the user.</param>
        /// <returns>
        /// A Task representing the asynchronous operation. 
        /// The method returns a boolean indicating whether the user creation was successful.
        /// </returns>
        /// <remarks>
        /// This method creates a new User instance with the provided username, password, email, and profile description.
        /// It then adds the new user to the database asynchronously.
        /// If the user creation is successful, the method returns true; otherwise, it logs an error using the ILogger and returns false.
        /// Note: It is recommended to encrypt or hash the password for security. 
        /// </remarks>
        public async Task<bool> AddUser(string username, string password, string email/*, string profileDesc*/)
        {
            try
            {
                var newUser = new User
                {
                    Username = username,
                    Password = Encode(password),
                    Email = email,
                    //ProfileDesc = profileDesc
                };

                _dbContext.Users.Add(newUser);

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
        /// Retrieves a user from the database based on the specified user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        /// <returns>
        /// A Task representing the asynchronous operation. 
        /// The method returns a nullable User object. 
        /// If the user with the specified ID is found, the object is returned; otherwise, it returns null.
        /// </returns>
        /// <remarks>
        /// This method searches for a user in the database using the provided user ID asynchronously.
        /// If the user is found, it is returned; otherwise, null is returned.
        /// If an exception occurs during the process, it is logged using the ILogger.
        /// </remarks>
        public async Task<User?> GetUser(string email, string password)
        {
            try
            {
                var user = await _dbContext.Users
                    .Where(u => u.Email == email && u.Password == Encode(password))
                    .FirstOrDefaultAsync();
                    //var user = await _dbContext.Users.FindAsync(userId);
                    return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>
        /// A Task representing the asynchronous operation. 
        /// The method returns a nullable IEnumerable of User objects. 
        /// If users are found in the database, the collection is returned; otherwise, it returns null.
        /// </returns>
        /// <remarks>
        /// This method retrieves all users from the database asynchronously.
        /// If users are found, the collection is returned; otherwise, null is returned.
        /// If an exception occurs during the process, it is logged using the ILogger.
        /// </remarks>
        public async Task<IEnumerable<User>?> GetAllUsers()
        {
            try
            {
                var users = await _dbContext.Users.ToListAsync();
                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Adds a game for a user in the database.
        /// </summary>
        /// <param name="userId">The ID of the user to whom the game will be added.</param>
        /// <param name="gameId">The ID of the game to be added for the user.</param>
        /// <returns>
        /// A Task representing the asynchronous operation. 
        /// The method returns true if the game is successfully added for the user; otherwise, false.
        /// </returns>
        /// <remarks>
        /// This method checks if the user with the specified ID and the game with the specified ID exist in the database.
        /// If both user and game are found, a new UserGame relationship is created and added to the database.
        /// The changes are then saved to the database asynchronously.
        /// If the process is successful, the method returns true; otherwise, it logs an error using the ILogger and returns false.
        /// </remarks>
        public async Task<bool> AddGameForUser(int userId, int gameId)
        {
            try
            {
                var user = await _dbContext.Users.FindAsync(userId);

                if (user == null) return false;

                var game = await _dbContext.Games.FindAsync(gameId);

                if (game == null) return false;

                var userGame = new UserGame
                {
                    Owner = user,
                    Game = game
                };

                _dbContext.UserGames.Add(userGame);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> UpdateUser(int id, string newUsername, string newPassword, string newEmail, string newProfileDesc)
        {
            try
            {
                var existingUser = await _dbContext.Users.FindAsync(id);

                if (existingUser == null)
                    return false;

                existingUser.Username = newUsername;
                existingUser.Password = newPassword;
                existingUser.Email = newEmail;
                existingUser.ProfileDesc = newProfileDesc;

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                var userToDelete = await _dbContext.Users.FindAsync(id);

                if (userToDelete == null)
                    return false;

                _dbContext.Users.Remove(userToDelete);

                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public static string Encode(string password)
        {
            try
            {
                byte[] EncDataByte = new byte[password.Length];
                EncDataByte = System.Text.Encoding.UTF8.GetBytes(password);
                string EncryptedData = Convert.ToBase64String(EncDataByte);

                return EncryptedData;
            }
            catch (Exception ex)
            {

                throw new Exception("Error in Encode:", ex);
            }
        }

        public static string Decode(string encryptedData)
        {
            try
            {
                System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
                System.Text.Decoder uTF8Decoder = encoder.GetDecoder();
                byte[] decodeByte = Convert.FromBase64String(encryptedData);

                //get the estimated number of characters that will be produced by decoding the specified byte array
                int charCount = uTF8Decoder.GetCharCount(decodeByte, 0, decodeByte.Length);

                char[] decodeChar = new char[charCount];
                uTF8Decoder.GetChars(decodeByte, 0, decodeByte.Length, decodeChar, 0);
                string decryptedData = new string(decodeChar);

                return decryptedData;
            }
            catch (Exception ex)
            {

                throw new Exception("Error in Encode:", ex);
            }
        }
    }
}
