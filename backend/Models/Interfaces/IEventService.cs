using Microsoft.AspNetCore.Mvc;

namespace VGL_Project.Models.Interfaces
{
    public interface IEventService
    {
        public Task<bool> AddEvent(string description, DateTime date, int gameId, int authorId, byte[] eventImage);
        public Task<Event?> GetEvent(int id);
        public Task<IEnumerable<Event>?> GetEvents();
        public Task<bool> UpdateEvent(int id, string description);
        public Task<bool> UpdateEvent(int id, DateTime date);
        public Task<bool> DeleteEvent(int id);
    }
}
