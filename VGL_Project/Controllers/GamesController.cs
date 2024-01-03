using Microsoft.AspNetCore.Mvc;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using VGL_Project.Models.Constants;
using VGL_Project.Models.Interfaces;

namespace VGL_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("add-game")]
        public async Task<IActionResult> AddGame(string title, string gameDesc, string genre)
        {
            await _gameService.AddGame(title, gameDesc, genre);

            return Ok("Game Created");
        }
        [HttpGet("get-all-owned-games-for-user")]
        public async Task<IActionResult> GetAllGames(ulong steamID,bool includeAppInfo = false,bool includeFreeGames = false)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(Constants.API_KEY);
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<PlayerService>(new HttpClient());
            var ownedGamesResponse = await steamInterface.GetOwnedGamesAsync(steamID, includeAppInfo, includeFreeGames);

            if (ownedGamesResponse == null) return NotFound();

            var data = ownedGamesResponse.Data;
            return Ok(data);
        }
        [HttpGet("get-game-by-appId")]
        public async Task<IActionResult> GetGameByID(uint appID)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(Constants.API_KEY);
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamApps>(new HttpClient());
            var ownedGamesResponse = await steamInterface.GetAppListAsync();

            if (ownedGamesResponse == null) return NotFound();

            var app = ownedGamesResponse.Data.FirstOrDefault(x => x.AppId == appID);

            if (app == null) return NotFound();

            return Ok(app);
        }

        [HttpGet("get-game-by-id")]
        public async Task<IActionResult> GetGame(int id)
        {
            var result = await _gameService.GetGame(id);

            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpGet("get-all-games")]
        public async Task<IActionResult> GetGames()
        {
            return Ok(await _gameService.GetGames());
        }

        [HttpPut("update-game/{id}")]
        public async Task<IActionResult> UpdateGame(int id, string newTitle, string newGameDesc, string newGenre)
        {
            var isUpdated = await _gameService.UpdateGame(id, newTitle, newGameDesc, newGenre);

            if (!isUpdated) return NotFound(); // Or return a BadRequest or any other appropriate status

            return Ok("Game Updated");
        }

        [HttpDelete("delete-game/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var result = await _gameService.DeleteGame(id);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }
    }
}
