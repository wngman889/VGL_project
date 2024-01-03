using Microsoft.AspNetCore.Mvc;
using SteamWebAPI2.Interfaces;
using SteamWebAPI2.Utilities;
using VGL_Project.Models.Interfaces;
using VGL_Project.Models.Constants;

namespace VGL_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddUser(string username, string password, string email/*, string profileDesc*/)
        {
            var result = await _userService.AddUser(username, password, email/*, profileDesc*/);

            if(!result) return BadRequest();

            return Ok("User Created");
        }

        [HttpGet("login")]
        public async Task<IActionResult> GetUser(string email, string password)
        {
            var result = await _userService.GetUser(email, password);

            if (result == null) return NotFound();

            return Ok(result);
        }
        [HttpGet("get-user-by-steamID")]
        public async Task<IActionResult> GetUserByID(ulong steamID)
        {
            var webInterfaceFactory = new SteamWebInterfaceFactory(Constants.API_KEY);
            var steamInterface = webInterfaceFactory.CreateSteamWebInterface<SteamUser>(new HttpClient());
            var playerSummaryResponse = await steamInterface.GetPlayerSummaryAsync(steamID);

            if(playerSummaryResponse == null) return NotFound();

            var playerSummaryData = playerSummaryResponse.Data;

            return Ok(playerSummaryData);
        }

        [HttpGet("get-all-users")]
        public async Task<IActionResult> GetUsers()
        {
            return Ok(await _userService.GetAllUsers());
        }

        [HttpPost("add-game-for-user")]
        public async Task<IActionResult> AddGameForUser(int userId, int gameId)
        {
            var result = await _userService.AddGameForUser(userId,gameId);

            if (!result) return BadRequest();

            return Ok("Game Added");
        }
        [HttpPut("update-user/{id}")]
        public async Task<IActionResult> UpdateUser(int id, string newUsername, string newPassword, string newEmail, string newProfileDesc)
        {
            var isUpdated = await _userService.UpdateUser(id, newUsername, newPassword, newEmail, newProfileDesc);

            if (!isUpdated) return NotFound();

            return Ok("User Information Updated");
        }

        [HttpDelete("delete-user/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);

            if (result)
                return Ok(result);
            else
                return NotFound();
        }
    }
}
