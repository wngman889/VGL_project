using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGL_Project.Controllers;
using VGL_Project.Models;
using VGL_Project.Models.Interfaces;
using VGL_Project.Services;

namespace VGL_Project.Tests.Controller
{
    public class EventsControllerTests
    {
        private readonly IEventService _eventService;

        private readonly ILogger<EventService> _logger;

        public EventsControllerTests()
        {
            _eventService = A.Fake<IEventService>();
            _logger = A.Fake<ILogger<EventService>>();
        }

        [Fact]
        public async Task EventsController_GetEvents_ReturnListOfEvents()
        {
            // Arrange
            var events = A.CollectionOfFake<Event>(3);
            var controller = new EventsController(_eventService, _logger);

            // Act
            var result = await controller.GetEvents();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<Event>>();
        }

        [Fact]
        public async Task EventsController_GetEvents_ReturnCorrectNumberOfEvents()
        {
            // Arrange
            var events = A.CollectionOfFake<Event>(6);
            A.CallTo(() => _eventService.GetEvents()).Returns(events);
            var controller = new EventsController(_eventService, _logger);

            // Act
            var result = await controller.GetEvents();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var eventList = okResult.Value as IEnumerable<Event>;
            eventList.Should().NotBeNull().And.HaveCount(6);
        }

        [Fact]
        public async Task EventsController_GetEvents_ReturnEmptyListWhenNoEvents()
        {
            // Arrange
            var emptyEvents = new List<Event>(); // An empty list
            A.CallTo(() => _eventService.GetEvents()).Returns(emptyEvents);
            var controller = new EventsController(_eventService, _logger);

            // Act
            var result = await controller.GetEvents();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var eventList = okResult.Value as IEnumerable<Event>;
            eventList.Should().NotBeNull().And.BeEmpty();
        }

        // Create events
        [Fact]
        public async Task AddEvent_ValidInput_ReturnOkResult()
        {
            // Arrange
            var controller = new EventsController(_eventService, _logger);
            var description = "Test Event";
            var date = DateTime.Now;
            var gameId = 1;
            var authorId = 1;

            // Act
            var result = await controller.AddEvent(description, date, gameId, authorId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Event Created");
        }

        [Fact]
        public async Task AddEvent_ReturnServerCode200()
        {
            // Arrange
            var controller = new EventsController(_eventService, _logger);
            var description = "Test description for unit test";
            var date = DateTime.Now;
            var gameId = 1;
            var authorId = 1;

            // Act
            var result = await controller.AddEvent(description, date, gameId, authorId);

            // Assert
            result.Should().NotBeNull();  // Add this line to assert not null
            result.Should().BeOfType<OkObjectResult>().Which.StatusCode.Should().Be(200);
        }

        // Test for logging an error when adding an event fails
        [Fact]
        public async Task AddEvent_ExceptionOccurs_LogsError()
        {
            // Arrange
            var description = "Test description for logs errors";
            var date = DateTime.Now;
            int gameId = 1;
            int authorId = 1;

            // Force an exception to occur during the service operation
            A.CallTo(() => _eventService.AddEvent(description, date, gameId, authorId))
                .Throws<Exception>();

            // Act
            var result = await _eventService.AddEvent("Test Event", DateTime.Now, 1, 1);

            // Assert
            result.Should().BeFalse();
        }


        //Get event
        [Fact]
        public async Task GetEvent_ValidId_ReturnOkResult()
        {
            // Arrange
            var controller = new EventsController(_eventService, _logger);
            var eventId = 1;

            // Mock the behavior of _eventService.GetEvent to return a valid event
            var fakeEvent = A.Fake<Event>();
            A.CallTo(() => _eventService.GetEvent(eventId)).Returns(fakeEvent);

            // Act
            var result = await controller.GetEvent(eventId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeEvent);
        }

        //Delete event
        [Fact]
        public async Task DeleteEvent_ReturnOkResult()
        {
            // Arrange
            var controller = new EventsController(_eventService, _logger);
            var eventId = 1;

            // Mock the behavior of _eventService.GetEvent to return a valid event
            var fakeEvent = A.Fake<Event>();
            A.CallTo(() => _eventService.DeleteEvent(eventId)).Returns(Task.FromResult(true));

            // Act
            var result = await controller.DeleteEvent(eventId);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("Event Deleted");
        }
    }
}
