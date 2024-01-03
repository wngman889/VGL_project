using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VGL_Project.Data;
using VGL_Project.Models;
using VGL_Project.Models.Interfaces;

namespace VGL_Project.Services
{
    public class EventService : IEventService
    {
        private readonly VGLDbContext _dbContext;

        private readonly ILogger<EventService> _logger;

        public EventService(VGLDbContext dbContext, ILogger<EventService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<bool> AddEvent(string description, DateTime date, int gameId, int authorId)
        {
            if (string.IsNullOrEmpty(description) || date < default(DateTime))
            {
                _logger.LogError("Invalid input parameters for adding an event");
                return false;
            }

            try
            {
                var game = await _dbContext.Games.FindAsync(gameId);
                var user = await _dbContext.Users.FindAsync(authorId);

                if (game == null || user == null)
                    return false;

                // Create a new Event instance
                var newEvent = new Event
                {
                    Description = description,
                    Date = date,
                    Game = game,// required relationships
                    Author = user
                };

                // Add the new event to the DbContext
                _dbContext.Events.Add(newEvent);

                // Save changes to the database
                await _dbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding event to the database");
                return false;
            }
        }

        public async Task<Event?> GetEvent(int id)//TODO: check if the event will be searched only by id
        {
            try
            {
                var eventFromDB = await _dbContext.Events.FindAsync(id);
                return eventFromDB;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while searching for events");
                return null;
            }
        }

        public async Task<IEnumerable<Event>?> GetEvents()
        {
            try
            {
                var events = await _dbContext.Events.ToListAsync();
                return events;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while getting the events");
                return null;
            }
        }

        public async Task<bool> UpdateEvent(int id, string description)
        {
            var existingEvent = await _dbContext.Events.FindAsync(id);

            if (existingEvent == null)
                return false;

            // Apply the updates from the DTO to the existing event
            if (!string.IsNullOrEmpty(description))
            {
                existingEvent.Description = description;
            }

            // Apply other update logic based on your requirements

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            return true;
        }
        //possible overloading
        public async Task<bool> UpdateEvent(int id, DateTime date)
        {
            var existingEvent = await _dbContext.Events.FindAsync(id);

            if (existingEvent == null)
                return false;

            if (date <= default(DateTime))
            {
                throw new Exception("Invalid date");
            }
            existingEvent.Date = date;

            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteEvent(int id)
        {
            var existingEvent = await _dbContext.Events.FindAsync(id);

            if (existingEvent == null)
                return false;

            _dbContext.Events.Remove(existingEvent);

            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
