using MySql.Data.MySqlClient;

namespace VGL_Project.Models.Interfaces
{
    public interface IUserService
    {
        public Task<bool> AddUser(string username, string password, string email, string steamId);
        //public Task<Response> Registration(User user, MySqlConnection connection);
        //public Task<Response> Login(User user, MySqlConnection connection);
        public Task<User?> GetUser(string email, string password);
        public Task<IEnumerable<User>?> GetAllUsers();
        public Task<bool> UpdateUser(int id, string newUsername, string newPassword, string newEmail, string steamId);
        Task<bool> DeleteUser(int id);

        public Task<bool> AddGameForUser(int userId, int gameId);
    }
}
