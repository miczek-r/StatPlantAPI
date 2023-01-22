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
    public class TestSensorTypeController
    {
        [Fact]
        public async Task GetByDay_ShouldReturn200Status()
        {
            var sensorTypeService = new Mock<ISensorTypeService>();
            sensorTypeService.Setup(_ => _.GetAll()).ReturnsAsync(SensorTypeMockData.GetSensorTypes());
            var sut = new SensorTypeController(sensorTypeService.Object);

            var result = (OkObjectResult?)(await sut.GetAll()).Result;

            result.StatusCode.Should().Be(200);
        }
    }
}
