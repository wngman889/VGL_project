using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
