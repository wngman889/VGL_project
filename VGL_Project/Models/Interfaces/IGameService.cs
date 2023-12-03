namespace VGL_Project.Models.Interfaces
{
    public interface IGameService
    {
        public Task<bool> AddGame(string title, string gameDesc, string genre);
        public Task<Game?> GetGame(int id);
        public Task<IEnumerable<Game>?> GetGames();
    }
}
