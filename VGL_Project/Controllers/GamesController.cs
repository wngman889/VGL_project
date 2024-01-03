using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using VGL_Project.Models;
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

        [HttpPost]
        public async Task<IActionResult> AddGame(string title, string gameDesc, string genre)
        {
            await _gameService.AddGame(title, gameDesc, genre);

            return Ok("Game Created");
        }

        [HttpGet("get-game-by-id")]
        public async Task<IActionResult> GetGame(int id)
        {
            var result = await _gameService.GetGame(id);

            if(result == null) return NotFound();

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
