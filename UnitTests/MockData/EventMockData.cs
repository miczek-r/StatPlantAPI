using Application.DTOs.Event;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class EventMockData
    {
        public static EventBaseDTO GetEvent()
        {
            return new EventBaseDTO
            {
                Title = "TestEvent",
                Details = "Test Event Details",
                StartTime = DateTime.Now,
                EndTime = null,
            };
        }

        public static List<EventLiteDTO> GetEventsByMonth()
        {
            return new List<EventLiteDTO>
            {
                new EventLiteDTO
                {
                    Date = DateTime.Now,
                    NumberOfEvents = 3
                }
            };
        }

        public static List<EventBaseDTO> GetEventsByDay()
        {
            return new List<EventBaseDTO>
            {
                new EventBaseDTO
                {
                    Title = "TestEvent",
                    Details = "Test Event Details",
                    StartTime = DateTime.Now,
                    EndTime = null,
                },
                 new EventBaseDTO
                {
                    Title = "TestEvent2",
                    Details = "Test Event 2 Details",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                }
            };
        }

        public static EventCreateDTO NewEvent()
        {
            return new EventCreateDTO
            {
                Title = "Test New Event",
                Details = "Test New Event Details",
                StartTime = DateTime.Now,
                EndTime = null
            };
        }

        public static EventGetByDateDTO NewGetEventByDateRequest()
        {
            return new EventGetByDateDTO
            {
                Date = DateTime.Now
            };
        }

        public static List<Event> GetEventEntities()
        {
            return new List<Event>
            {
                new Event
                {
                    Title = "Test Event 1",
                    Details = "Test Event 1 Details",
                    StartTime = DateTime.Now,
                    EndTime = null,
                    UserId = "testUserId1",
                    IsNotificationSent = false,
                }, new Event
                {
                    Title = "Test Event 2",
                    Details = "Test Event 2 Details",
                    StartTime = DateTime.Now,
                    EndTime = DateTime.Now,
                    UserId = "testUserId2",
                    IsNotificationSent = false,
                }, new Event
                {
                    Title = "Test Event 3",
                    Details = "Test Event 3 Details",
                    StartTime = DateTime.Now,
                    EndTime = null,
                    UserId = "testUserId3",
                    IsNotificationSent = false,
                },
            };
        }

        public static Event NewEventEntity()
        {
            return new Event
            {
                Title = "Test Event 4",
                Details = "Test Event 4 Details",
                StartTime = DateTime.Now,
                EndTime = null,
                UserId = "testUserId4",
                IsNotificationSent = false,
            };
        }
    }
}
