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
    public class TestNotificationController
    {
        [Fact]
        public async Task Get_ShouldReturn200Status()
        {
            var notificationService = new Mock<INotificationService>();
            notificationService.Setup(_ => _.GetAll()).ReturnsAsync(NotificationMockData.GetNotifications());
            var sut = new NotificationController(notificationService.Object);

            var result = (OkObjectResult?)(await sut.GetAll()).Result;

            result.StatusCode.Should().Be(200);
        }
    }
}
