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
    public class TestDeviceController
    {
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            var deviceService = new Mock<IDeviceService>();
            deviceService.Setup( _ =>  _.GetById(2)).ReturnsAsync( DeviceMockData.GetDevices().First());
            var sut = new DeviceController(deviceService.Object);

            var result = (OkObjectResult)(await sut.Get(2)).Result;

            result.StatusCode.Should().Be(200);
        }
    }
}
