
using Microsoft.AspNetCore.Mvc;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using VGL_Project.Models.Constants;
using VGL_Project.Models.Interfaces;
using VGL_Project.Models.DTO;
using System.Linq;
using Newtonsoft.Json;

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
        [HttpPost("get-all-owned-games-for-user")]
        public async Task<IActionResult> GetAllGames([FromBody]OwnedGamesForUserDTO ownedGames)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(Constants.API_KEY);
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<PlayerService>(new HttpClient());
            var ownedGamesResponse = await steamInterface.GetOwnedGamesAsync(ulong.Parse(ownedGames.SteamId), ownedGames.IncludeAppInfo, ownedGames.IncludeFreeGames);

            if (ownedGamesResponse == null) return NotFound();

            var data = ownedGamesResponse.Data;
            return Ok(data);
        }
        [HttpPost("get-most-played-owned-games")]
        public async Task<IActionResult> GetMostPlayed([FromBody] MostPlayedGamesDTO mostPlayed)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(Constants.API_KEY);
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<PlayerService>(new HttpClient());
            var ownedGamesResponse = await steamInterface.GetOwnedGamesAsync(ulong.Parse(mostPlayed.SteamId), false, true);
            if (ownedGamesResponse == null) return NotFound();

            var data = ownedGamesResponse.Data;

            var mostPlayedGames = data.OwnedGames
            .OrderByDescending(x => x.PlaytimeForever)
            .Take(mostPlayed.Count)
            .ToList();

            var gameDetailsList = new List<object>();

            foreach (var game in mostPlayedGames)
            {
                var gameDetailsResponse = await GetGameDetails((int)game.AppId);

                if (gameDetailsResponse is OkObjectResult okResult)
                {
                    gameDetailsList.Add(okResult.Value);
                }
            }

            return Ok(gameDetailsList);
        }
        [HttpPost("get-random-gems-owned-games")]
        public async Task<IActionResult> GetRandomGems([FromBody] MostPlayedGamesDTO mostPlayed)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(Constants.API_KEY);
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<PlayerService>(new HttpClient());
            var ownedGamesResponse = await steamInterface.GetOwnedGamesAsync(ulong.Parse(mostPlayed.SteamId), false, true);

            if (ownedGamesResponse == null) return NotFound();

            var ownedGames = ownedGamesResponse.Data.OwnedGames.ToList();

            var random = new Random();
            var shuffledGames = ownedGames.OrderBy(x => random.Next()).ToList();

            var randomGames = shuffledGames
                .Take(mostPlayed.Count)
                .ToList();

            var gameDetailsList = new List<object>();

            foreach (var game in randomGames)
            {
                var gameDetailsResponse = await GetGameDetails((int)game.AppId);

                if (gameDetailsResponse is OkObjectResult okResult)
                {
                    gameDetailsList.Add(okResult.Value);
                }
            }

            return Ok(gameDetailsList);
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

        [HttpPost("get-gamenews-for-random-game")]
        public async Task<IActionResult> GetNewsGameByID([FromBody] NewsDTO news)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(Constants.API_KEY);
            var steamInterfacePlayer = webInterfaceFactory.CreateSteamWebInterface<PlayerService>(new HttpClient());

            // Fetch user's owned games
            var ownedGamesResponse = await steamInterfacePlayer.GetOwnedGamesAsync(ulong.Parse(news.SteamId), false, false);

            if (ownedGamesResponse == null || ownedGamesResponse.Data == null || !ownedGamesResponse.Data.OwnedGames.Any())
                return NotFound("No owned games found for the user.");

            // Get a random game from the owned games
            var randomGame = ownedGamesResponse.Data.OwnedGames.OrderBy(g => Guid.NewGuid()).FirstOrDefault();

            if (randomGame == null)
                return NotFound("No random game found.");

            var appID = randomGame.AppId;

            // Fetch news for the random game
            var steamInterfaceNews = webInterfaceFactory.CreateSteamWebInterface<SteamNews>(new HttpClient());
            var response = await steamInterfaceNews.GetNewsForAppAsync(appID,default,default,(uint)news.MaxCount);

            if (response == null)
                return NotFound("No news found for the game.");

            return Ok(response.Data);
        }
        [HttpPost("get-app-details")]
        public async Task<IActionResult> GetGameDetails(int id)
        {
            try
            {
                var apiUrl = $"https://store.steampowered.com/api/appdetails?appids={id}&l=en";

                using (HttpClient httpClient = new HttpClient())
                {
                    var response = await httpClient.GetStringAsync(apiUrl);

                    // Deserialize the JSON response into a C# object
                    var gameDetails = JsonConvert.DeserializeObject<Dictionary<string, GameDetails>>(response);

                    // Check if the request was successful
                    if (gameDetails.TryGetValue(id.ToString(), out var details) && details.Success)
                    {
                        var data = details.Data;
                        var result = new
                        {
                            AppId = data.AppId,
                            Name = data.Name,
                            Developer = data.Developers[0],
                            Genre = data.Genres?.FirstOrDefault()?.Description,
                            ReleaseDate = data.ReleaseDate.Date.ToShortDateString(),
                            Description = details.Data.SanitizedDetailedDescription
                        };

                        return Ok(result);
                    }
                    else
                    {
                        return Ok();
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
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
