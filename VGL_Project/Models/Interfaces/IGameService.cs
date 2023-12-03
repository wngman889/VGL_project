namespace VGL_Project.Models.Interfaces
{
    public interface IGameService
    {
        public Task AddGame(string title, string gameDesc, string genre);
    }
}
