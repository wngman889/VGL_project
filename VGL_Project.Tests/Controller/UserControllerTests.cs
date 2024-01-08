using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
