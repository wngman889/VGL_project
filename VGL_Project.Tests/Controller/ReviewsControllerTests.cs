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
using VGL_Project.Data;
using VGL_Project.Models;
using VGL_Project.Models.Interfaces;
using VGL_Project.Services;

namespace VGL_Project.Tests.Controller
{
    public class ReviewsControllerTests
    {
        private readonly IReviewService _reviewService;

        public ReviewsControllerTests()
        {
            _reviewService = A.Fake<IReviewService>();
        }

        [Fact]
        public async Task GetReviews_ReturnsOkResultWithReviews()
        {
            // Arrange
            var reviews = A.CollectionOfFake<ReviewRecommendation>(3);
            var controller = new ReviewsController(_reviewService);

            // Act
            var result = await controller.GetReviews();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<ReviewRecommendation>>();
        }

        [Fact]
        public async Task GetReviews_ReturnsEmptyList()
        {
            // Arrange
            var emptyReviews = new List<ReviewRecommendation>(); // An empty list
            A.CallTo(() => _reviewService.GetReviews()).Returns(emptyReviews);
            var controller = new ReviewsController(_reviewService);

            // Act
            var result = await controller.GetReviews();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var reivewList = okResult.Value as IEnumerable<ReviewRecommendation>;
            reivewList.Should().NotBeNull().And.BeEmpty();
        }

        [Fact]
        public async Task ReviewsController_ReturnCorrectNumbers()
        {
            // Arrange
            var reviews = A.CollectionOfFake<ReviewRecommendation>(6);
            A.CallTo(() => _reviewService.GetReviews()).Returns(reviews);
            var controller = new ReviewsController(_reviewService);

            // Act
            var result = await controller.GetReviews();

            // Assert
            result.Should().NotBeNull().And.BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            var reviewList = okResult.Value as IEnumerable<ReviewRecommendation>;
            reviewList.Should().NotBeNull().And.HaveCount(6);
        }
    }
}
