using Application.DTOs.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class UserMockData
    {
        public static LoginDTO NewLoginRequest()
        {
            return new LoginDTO
            {
                Email = "test@test.com",
                Password = "!Test123"
            };
        }
        public static EmailConfirmationDTO NewConfirmEmailRequest()
        {
            return new EmailConfirmationDTO
            {
                UserId = "testUserId",
                ConfirmationToken = "testConfirmationToken"
            };
        }
        public static LoginResponseDTO GetLoginResult()
        {
            return new LoginResponseDTO(new TokenInfoDTO
            {
                Token = "testToken",
                TokenValidUntil = new DateTime()
            });
        }
    }
}
