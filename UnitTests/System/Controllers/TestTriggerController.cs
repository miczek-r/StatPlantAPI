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
    public class TestTriggerController
    {
        [Fact]
        public async Task GetByDeviceId_ShouldReturn200Status()
        {
            var triggerService = new Mock<ITriggerService>();
            triggerService.Setup(_ => _.GetAll(1)).ReturnsAsync(TriggerMockData.GetTriggers());
            var sut = new TriggerController(triggerService.Object);

            var result = (OkObjectResult?)(await sut.GetByDeviceId(1)).Result;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetById_ShouldReturn200Status()
        {
            var triggerService = new Mock<ITriggerService>();
            triggerService.Setup(_ => _.Get(1)).ReturnsAsync(TriggerMockData.GetTrigger());
            var sut = new TriggerController(triggerService.Object);

            var result = (OkObjectResult?)(await sut.GetById(1)).Result;

            result.StatusCode.Should().Be(200);
        }
        [Fact]
        public async Task Create_ShouldReturn201Status()
        {
            var triggerService = new Mock<ITriggerService>();
            var newTrigger = TriggerMockData.NewTrigger();
            triggerService.Setup(_ => _.Get(1)).ReturnsAsync(TriggerMockData.GetTrigger());
            var sut = new TriggerController(triggerService.Object);

            var result = await sut.Create(newTrigger);

            triggerService.Verify(_ => _.Create(newTrigger), Times.Exactly(1));
            ((CreatedAtActionResult?)result.Result).StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task Update_ShouldReturn204Status()
        {
            var triggerService = new Mock<ITriggerService>();
            var triggerToUpdate = TriggerMockData.UpdateTrigger();
            var sut = new TriggerController(triggerService.Object);

            var result = await sut.Update(triggerToUpdate);

            triggerService.Verify(_ => _.Update(triggerToUpdate), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Delete_ShouldReturn204Status()
        {
            var triggerService = new Mock<ITriggerService>();
            var sut = new TriggerController(triggerService.Object);

            var result = await sut.Delete(123);

            triggerService.Verify(_ => _.Remove(123), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task ChackAllTriggers_ShouldReturn204Status()
        {
            var triggerService = new Mock<ITriggerService>();
            var sut = new TriggerController(triggerService.Object);

            var result = await sut.CheckAllTriggers();

            triggerService.Verify(_ => _.CheckAllTriggers(), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }
    }
}
