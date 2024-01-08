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
using VGL_Project.Models.DTO;
using VGL_Project.Models.Interfaces;
using VGL_Project.Services;

namespace VGL_Project.Tests.Controller
{
    public class UserControllerTests
    {
        private readonly IUserService _userService;

        public UserControllerTests()
        {
            _userService = A.Fake<IUserService>();
        }

        [Fact]
        public async Task GetUsers_ReturnsOkResult()
        {
            // Arrange
            var users = A.CollectionOfFake<User>(3);
            var controller = new UserController(_userService);

            // Act
            var result = await controller.GetUsers();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<User>>();
        }

        [Fact]
        public async Task AddUser_ReturnsOkObjectResultOnSuccess()
        {
            // Arrange
            A.CallTo(() => _userService.AddUser(A<string>._, A<string>._, A<string>._, A<string>._)).Returns(true);
            var controller = new UserController(_userService);

            // Act
            var registerDTO = new RegisterDTO { Username = "TestUser", Password = "TestPassword", Email = "test@example.com", SteamId = "12345" };
            var result = await controller.AddUser(registerDTO);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("User Created");
        }

        [Fact]
        public async Task AddUser_ReturnsBadRequestResultOnFailure()
        {
            // Arrange
            A.CallTo(() => _userService.AddUser(A<string>._, A<string>._, A<string>._, A<string>._)).Returns(false);
            var controller = new UserController(_userService);

            // Act
            var registerDTO = A.Fake<RegisterDTO>();
            var result = await controller.AddUser(registerDTO);

            // Assert
            result.Should().NotBeNull().And.BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task GetUser_ReturnsOkObjectResultOnSuccess()
        {
            // Arrange
            var fakeUser = A.Fake<User?>();
            A.CallTo(() => _userService.GetUser(A<string>._, A<string>._)).Returns(Task.FromResult(fakeUser));
            var controller = new UserController(_userService);

            // Act
            var loginDTO = A.Fake<LoginDTO>();
            var result = await controller.GetUser(loginDTO);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeEquivalentTo(fakeUser);
        }

        [Fact]
        public async Task GetUser_ReturnsNotFoundResultOnUserNotFound()
        {
            // Arrange
            A.CallTo(() => _userService.GetUser(A<string>._, A<string>._)).Returns(Task.FromResult<User?>(null));
            var controller = new UserController(_userService);

            // Act
            var loginDTO = A.Fake<LoginDTO>();
            var result = await controller.GetUser(loginDTO);

            // Assert
            result.Should().NotBeNull().And.BeOfType<NotFoundResult>();
        }

        //update user
        [Fact]
        public async Task UpdateUser_ReturnsOkObjectResultOnSuccess()
        {
            // Arrange
            A.CallTo(() => _userService.UpdateUser(A<int>._, A<string>._, A<string>._, A<string>._, A<string>._))
                .Returns(Task.FromResult(true));
            var controller = new UserController(_userService);

            // Act
            var result = await controller.UpdateUser(1, "NewUsername", "NewPassword", "new@example.com", "New Profile Description");

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be("User Information Updated");
        }

        [Fact]
        public async Task UpdateUser_ReturnsNotFoundResultOnUserNotFound()
        {
            // Arrange
            A.CallTo(() => _userService.UpdateUser(A<int>._, A<string>._, A<string>._, A<string>._, A<string>._))
                .Returns(Task.FromResult(false));
            var controller = new UserController(_userService);

            // Act
            var result = await controller.UpdateUser(1, "NewUsername", "NewPassword", "new@example.com", "New Profile Description");

            // Assert
            result.Should().NotBeNull().And.BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task DeleteUser_ReturnsOkObjectResultOnSuccess()
        {
            // Arrange;
            A.CallTo(() => _userService.DeleteUser(A<int>._)).Returns(Task.FromResult(true));
            var controller = new UserController(_userService);

            // Act
            var result = await controller.DeleteUser(1);

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task DeleteUser_ReturnsNotFoundResultOnUserNotFound()
        {
            // Arrange
            A.CallTo(() => _userService.DeleteUser(A<int>._)).Returns(Task.FromResult(false));
            var controller = new UserController(_userService);

            // Act
            var result = await controller.DeleteUser(1);

            // Assert
            result.Should().NotBeNull().And.BeOfType<NotFoundResult>();
        }
    }
}
