namespace VGL_Project.Models.Interfaces
{
    public interface IGameService
    {
        public Task<bool> AddGame(string title, string gameDesc, string genre);
        public Task<Game?> GetGame(int id);
        Task<bool> UpdateGame(int id, string newTitle, string newGameDesc, string newGenre);
        Task<bool> DeleteGame(int id);
        public Task<IEnumerable<Game>?> GetGames();
    }
}
