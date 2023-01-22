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
    public class TestHubController
    {
        [Fact]
        public async Task GetAll_ShouldReturn200Status()
        {
            var hubService = new Mock<IHubService>();
            hubService.Setup(_ => _.GetAll()).ReturnsAsync(HubMockData.GetHubs());
            var sut = new HubController(hubService.Object);

            var result = (OkObjectResult?)(await sut.GetAll()).Result;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            var hubService = new Mock<IHubService>();
            hubService.Setup(_ => _.GetById(1)).ReturnsAsync(HubMockData.GetHub());
            var sut = new HubController(hubService.Object);

            var result = (OkObjectResult?)(await sut.Get(1)).Result;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Create_ShouldReturn201Status()
        {
            var hubService = new Mock<IHubService>();
            var newHub = HubMockData.NewHub();
            hubService.Setup(_ => _.GetById(1)).ReturnsAsync(HubMockData.GetHub());
            var sut = new HubController(hubService.Object);

            var result = await sut.CreateHub(newHub);

            hubService.Verify(_ => _.Create(newHub), Times.Exactly(1));
            ((CreatedAtActionResult?)result.Result).StatusCode.Should().Be(201);
        }
        [Fact]
        public async Task Join_ShouldReturn201Status()
        {
            var hubService = new Mock<IHubService>();
            var joinHubRequest = HubMockData.JoinHubRequest();
            var sut = new HubController(hubService.Object);

            var result = await sut.JoinHub(joinHubRequest);

            hubService.Verify(_ => _.Join(joinHubRequest), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }
        [Fact]
        public async Task Leave_ShouldReturn201Status()
        {
            var hubService = new Mock<IHubService>();
            var sut = new HubController(hubService.Object);

            var result = await sut.LeaveHub(1);

            hubService.Verify(_ => _.Leave(1), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }
    }
}
