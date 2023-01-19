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
    public class TestAuthenticationController
    {
        [Fact]
        public async Task Login_ShouldReturn200Status()
        {
            var authenticationService = new Mock<IAuthenticationService>();
            var loginRequest =UserMockData.NewLoginRequest();
            authenticationService.Setup(_ => _.Login(loginRequest)).ReturnsAsync(UserMockData.GetLoginResult());
            var sut = new AuthenticationController(authenticationService.Object);

            var result = (OkObjectResult?)(await sut.Login(loginRequest)).Result;

            result.StatusCode.Should().Be(200);
        }


        [Fact]
        public async Task ConfirmMail_ShouldReturn204Status()
        {
            var authenticationService = new Mock<IAuthenticationService>();
            var confirmEmailRequest = UserMockData.NewConfirmEmailRequest();
            var sut = new AuthenticationController(authenticationService.Object);

            var result = await sut.ConfirmMail(confirmEmailRequest);

            authenticationService.Verify(_ => _.ConfirmEmail(confirmEmailRequest), Times.Exactly(1));
            ((NoContentResult)result).StatusCode.Should().Be(204);
        }
    }
}
