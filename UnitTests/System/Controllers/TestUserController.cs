using Application.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StatPlantAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.MockData;
using Xunit;

namespace UnitTests.System.Controllers
{
    public class TestUserController
    {
        [Fact]
        public async Task GetAll_ShouldReturn200Status()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetAll()).ReturnsAsync(UserMockData.GetUsers());
            var sut = new UserController(userService.Object);

            var result = (OkObjectResult?)(await sut.GetAll()).Result;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetById_ShouldReturn200Status()
        {
            var userService = new Mock<IUserService>();
            userService.Setup(_ => _.GetById("testUserId1")).ReturnsAsync(UserMockData.GetUser());
            var sut = new UserController(userService.Object);

            var result = (OkObjectResult?)(await sut.Get("testUserId1")).Result;

            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Create_ShouldReturn201Status()
        {
            var userService = new Mock<IUserService>();
            var newUser = UserMockData.NewUser();
            userService.Setup(_ => _.GetById("testUserId1")).ReturnsAsync(UserMockData.GetUser());
            var sut = new UserController(userService.Object);

            var result = await sut.Register(newUser);

            userService.Verify(_ => _.Create(newUser), Times.Exactly(1));
            ((CreatedAtActionResult)result).StatusCode.Should().Be(201);
        }
    }
}
