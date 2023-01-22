using Application.DTOs.Authentication;
using Application.DTOs.User;
using Core.Entities;
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

        public static List<UserBaseDTO> GetUsers()
        {
            return new List<UserBaseDTO>
            {
                new UserBaseDTO
                {
                    Id = "testUserId1",
                    Email = "test1@user.com",
                    FirstName = "TestFirstName1",
                    LastName = "TestLastName1"
                },
                new UserBaseDTO
                {
                    Id = "testUserId2",
                    Email = "test2@user.com",
                    FirstName = "TestFirstName2",
                    LastName = "TestLastName2"
                },
                new UserBaseDTO
                {
                    Id = "testUserId3",
                    Email = "test3@user.com",
                    FirstName = "TestFirstName3",
                    LastName = "TestLastName3"
                }
            };
        }

        public static UserBaseDTO GetUser()
        {
            return new UserBaseDTO
            {
                Id = "testUserId1",
                Email = "test1@user.com",
                FirstName = "TestFirstName1",
                LastName = "TestLastName1"
            };
        }

        public static UserCreateDTO NewUser()
        {
            return new UserCreateDTO
            {
                Email = "test1@user.com",
                Password = "!TestUserPa$$1",
                FirstName = "TestFirstName1",
                LastName = "TestLastName1"
            };
        }

        public static User GetUserEntity()
        {
            return new User
            {
                Email = "test1@user.com",
                FirstName = "TestFirstName1",
                LastName = "TestLastName1",
                Hubs = new List<Hub>(),
                Notifications = NotificationMockData.GetNotificationEntities(),
                Events = EventMockData.GetEventEntities()
            };
        }

        public static List<User> GetUserEntities()
        {
            return new List<User>
            {
                new User
                {
                    Email = "test1@user.pl",
                    FirstName = "TestFirstName1",
                    LastName = "TestLastName1",
                    Hubs = new List<Hub>(),
                    Notifications = NotificationMockData.GetNotificationEntities(),
                    Events = EventMockData.GetEventEntities()
                }, new User
                {
                    Email = "test2@user.com",
                    FirstName = "TestFirstName2",
                    LastName = "TestLastName2",
                    Hubs = new List<Hub>(),
                    Notifications = NotificationMockData.GetNotificationEntities(),
                    Events = EventMockData.GetEventEntities()
                }, new User
                {
                    Email = "test3@user.com",
                    FirstName = "TestFirstName3",
                    LastName = "TestLastName3",
                    Hubs = new List<Hub>(),
                    Notifications = NotificationMockData.GetNotificationEntities(),
                    Events = EventMockData.GetEventEntities()
                },
            };
        }

        public static User NewUserEntity()
        {
            return new User
            {
                Email = "test4@user.com",
                FirstName = "TestFirstName4",
                LastName = "TestLastName4",
                Hubs = new List<Hub>(),
                Notifications = NotificationMockData.GetNotificationEntities(),
                Events = EventMockData.GetEventEntities()
            };
        }
    }
}
