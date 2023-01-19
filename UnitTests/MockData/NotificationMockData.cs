using Application.DTOs.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class NotificationMockData
    {
        public static List<NotificationBaseDTO> GetNotifications()
        {
            return new List<NotificationBaseDTO>
            {
                new NotificationBaseDTO
                {
                    Id = "testNotificationId1",
                    CreatedTime = DateTime.Now,
                    Status = Core.Enums.NotificationStatus.New,
                    Title = "Test Notification 1",
                    Text = "This is Test Notification 1"
                },
                 new NotificationBaseDTO
                {
                    Id = "testNotificationId2",
                    CreatedTime = DateTime.Now,
                    Status = Core.Enums.NotificationStatus.New,
                    Title = "Test Notification 2",
                    Text = "This is Test Notification 2"
                }
            };
        }
    }
}
