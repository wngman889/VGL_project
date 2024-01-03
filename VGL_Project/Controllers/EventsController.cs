using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using VGL_Project.Models;
using VGL_Project.Models.Interfaces;
using VGL_Project.Services;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace VGL_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        private readonly ILogger<EventService> _logger;
        public EventsController(IEventService eventService, ILogger<EventService> logger)
        {
            _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(string description, DateTime date, int gameId, int authorId)
        {
            await _eventService.AddEvent(description, date, gameId, authorId);

            return Ok("Event Created");
        }

        [HttpGet("get-event")]
        public async Task<IActionResult> GetEvent(int id)
        {
            try
            {
                var result = await _eventService.GetEvent(id);

                if (result == null)
                {
                    _logger.LogInformation($"Event with ID {id} not found.");
                    return NotFound();
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while retrieving event with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("get-all-events")]
        public async Task<IActionResult> GetEvents()
        {
            return Ok(await _eventService.GetEvents());
        }

        [HttpPut("update-event-description/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, string description)
        {
            try
            {
                var existingEvent = await _eventService.GetEvent(id);

                if (existingEvent == null)
                {
                    _logger.LogInformation($"Event with ID {id} not found.");
                    return NotFound();
                }

                await _eventService.UpdateEvent(id, description);

                return Ok("Event Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating event with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
        [HttpPut("update-event-date/{id}")]
        public async Task<IActionResult> UpdateEvent(int id, DateTime date)
        {
            try
            {
                var existingEvent = await _eventService.GetEvent(id);

                if (existingEvent == null)
                {
                    _logger.LogInformation($"Event with ID {id} not found.");
                    return NotFound();
                }

                await _eventService.UpdateEvent(id, date);

                return Ok("Event Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while updating event with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var existingEvent = await _eventService.GetEvent(id);

                if (existingEvent == null)
                {
                    _logger.LogInformation($"Event with ID {id} not found.");
                    return NotFound();
                }

                await _eventService.DeleteEvent(id);

                return Ok("Event Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError($"An error occurred while deleting event with ID {id}: {ex.Message}");
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
