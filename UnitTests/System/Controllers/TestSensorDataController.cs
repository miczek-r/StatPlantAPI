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
    public class TestSensorDataController
    {
        [Fact]
        public async Task Create_ShouldReturn201Status()
        {
            var sensorDataService = new Mock<ISensorDataService>();
            var newSensorData = SensorDataMockData.NewSensorData();
            var sut = new SensorDataController(sensorDataService.Object);

            var result = await sut.Post(newSensorData);

            sensorDataService.Verify(_ => _.Create(newSensorData), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            var sensorDataService = new Mock<ISensorDataService>();
            var getSensorDataDetailsRequest = SensorDataMockData.GetSensorDataDetailsRequest();
            sensorDataService.Setup(_ => _.GetDetails(getSensorDataDetailsRequest)).ReturnsAsync(SensorDataMockData.GetSensorDataDetails());
            var sut = new SensorDataController(sensorDataService.Object);

            var result = (OkObjectResult?)(await sut.Get(getSensorDataDetailsRequest)).Result;

            result.StatusCode.Should().Be(200);
        }

    }
}
