namespace VGL_Project.Models.Interfaces
{
    public interface IUserService
    {
        public Task<bool> AddUser(string username, string password, string email, string profileDesc);
        public Task<User?> GetUser(int userId);
        public Task<IEnumerable<User>?> GetAllUsers();
        public Task<bool> UpdateUser(int id, string newUsername, string newPassword, string newEmail, string newProfileDesc);
        Task<bool> DeleteUser(int id);

        public Task<bool> AddGameForUser(int userId, int gameId);
    }
}
