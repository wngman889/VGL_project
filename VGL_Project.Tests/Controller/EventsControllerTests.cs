using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
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
        public async Task EventsController_GetEvents_ReturnsListOfEvents()
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
        public async Task EventsController_GetEvents_ReturnsCorrectNumberOfEvents()
        {
            // Arrange
            var events = A.CollectionOfFake<Event>(6); // Adjust the number as needed
            A.CallTo(() => _eventService.GetEvents()).Returns(events);
            var controller = new EventsController(_eventService, _logger);

            // Act
            var result = await controller.GetEvents();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var eventList = okResult.Value as IEnumerable<Event>;
            eventList.Should().NotBeNull().And.HaveCount(6); // Adjust the count as needed
        }

        [Fact]
        public async Task EventsController_GetEvents_ReturnsEmptyListWhenNoEvents()
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
    }
}
