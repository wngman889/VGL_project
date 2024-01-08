using FakeItEasy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
