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
    public class TestEventController
    {
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            var eventService = new Mock<IEventService>();
            eventService.Setup(_ => _.Get(1)).ReturnsAsync(EventMockData.GetEvent());
            var sut = new EventController(eventService.Object);

            var result = (OkObjectResult?)(await sut.Get(1)).Result;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetByDay_ShouldReturn200Status()
        {
            var eventService = new Mock<IEventService>();
            var getByDateRequest = EventMockData.NewGetEventByDateRequest();
            eventService.Setup(_ => _.GetEventsForDay(getByDateRequest)).ReturnsAsync(EventMockData.GetEventsByDay());
            var sut = new EventController(eventService.Object);

            var result = (OkObjectResult?)(await sut.GetByDay(getByDateRequest)).Result;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetByMonth_ShouldReturn200Status()
        {
            var eventService = new Mock<IEventService>();
            var getByDateRequest = EventMockData.NewGetEventByDateRequest();
            eventService.Setup(_ => _.GetEventsForMonth(getByDateRequest)).ReturnsAsync(EventMockData.GetEventsByMonth());
            var sut = new EventController(eventService.Object);

            var result = (OkObjectResult?)(await sut.GetByMonth(getByDateRequest)).Result;

            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Create_ShouldReturn201Status()
        {
            var eventService = new Mock<IEventService>();
            var newEvent = EventMockData.NewEvent();
            eventService.Setup(_ => _.Get(1)).ReturnsAsync(EventMockData.GetEvent());
            var sut = new EventController(eventService.Object);

            var result = await sut.Create(newEvent);

            eventService.Verify(_ => _.Create(newEvent), Times.Exactly(1));
            ((CreatedAtActionResult?)result.Result).StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task Update_ShouldReturn204Status()
        {
            var eventService = new Mock<IEventService>();
            var eventToUpdate = EventMockData.GetEvent();
            var sut = new EventController(eventService.Object);

            var result = await sut.Update(eventToUpdate);

            eventService.Verify(_ => _.Update(eventToUpdate), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task Delete_ShouldReturn204Status()
        {
            var eventService = new Mock<IEventService>();
            var sut = new EventController(eventService.Object);

            var result = await sut.Delete(123);

            eventService.Verify(_ => _.Remove(123), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }

        [Fact]
        public async Task ChackAllEvents_ShouldReturn204Status()
        {
            var eventService = new Mock<IEventService>();
            var sut = new EventController(eventService.Object);

            var result = await sut.CheckAllEvents();

            eventService.Verify(_ => _.CheckAllEvents(), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }
    }
}
