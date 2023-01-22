using Application.DTOs.Condition;
using Application.DTOs.Trigger;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class TriggerMockData
    {
        public static List<TriggerLiteDTO> GetTriggers()
        {
            return new List<TriggerLiteDTO>
            {
                new TriggerLiteDTO
                {
                    Id=1,
                    Name="Test Trigger 1",
                    ConditionsCount = 1,
                    IsActive=true,
                },

                new TriggerLiteDTO
                {
                    Id=2,
                    Name="Test Trigger 2",
                    ConditionsCount = 6,
                    IsActive=true,
                },
                new TriggerLiteDTO
                {
                    Id=3,
                    Name="Test Trigger 3",
                    ConditionsCount = 1,
                    IsActive=false,
                }
            };
        }

        public static TriggerBaseDTO GetTrigger()
        {
            return new TriggerBaseDTO
            {
                Id = 1,
                Name = "Test Trigger 1",
                TriggerType = Core.Enums.TriggerType.BOTH,
                ApiUrl = "http://example.com",
                NotificationText = "Test Notification 1",
                IsActive = true,
                Conditions = new List<ConditionBaseDTO>
                {
                    new ConditionBaseDTO
                    {
                        Id = 1,
                        Inequality = Core.Enums.Inequality.LOWER,
                        SensorTypeId = 1,
                        Value = 4.5f
                    }
                }
            };
        }

        public static TriggerCreateDTO NewTrigger()
        {
            return new TriggerCreateDTO
            {
                DeviceId = 1,
                Name = "Test Trigger 1",
                TriggerType = Core.Enums.TriggerType.API_CALL,
                ApiUrl = "http://example.com",
                IsActive = true,
                Conditions = new List<ConditionUpdateDTO>
                {
                    new ConditionUpdateDTO
                    {
                        Inequality = Core.Enums.Inequality.EQUAL,
                        SensorTypeId = 1,
                        Value = 4.5f
                    }
                }
            };
        }
        public static TriggerUpdateDTO UpdateTrigger()
        {
            return new TriggerUpdateDTO
            {
                Id = 1,
                Name = "Test Trigger 1",
                TriggerType = Core.Enums.TriggerType.NOTIFICATION,
                NotificationText = "Test Notification 1",
                IsActive = true,
                Conditions = new List<ConditionUpdateDTO>
                {
                    new ConditionUpdateDTO
                    {
                        Inequality = Core.Enums.Inequality.HIGHER,
                        SensorTypeId = 1,
                        Value = 4.5f
                    }
                }
            };
        }

        public static Trigger GetTriggerEntity()
        {
            return new Trigger
            {
                Name = "Test Trigger 1",
                TriggerType = Core.Enums.TriggerType.API_CALL,
                ApiUrl = "http://example.com",
                NotificationText = "Test Notification 1",
                DeviceId = 1,
                Conditions = ConditionMockData.GetConditionEntities(),
                IsActive = true,
                HasBeenCalled = false
            };
        }

        public static List<Trigger> GetTriggerEntities()
        {
            return new List<Trigger>
            {
            new Trigger
            {
                Name = "Test Trigger 1",
                TriggerType = Core.Enums.TriggerType.API_CALL,
                ApiUrl = "http://example.com",
                DeviceId = 1,
                Conditions = ConditionMockData.GetConditionEntities(),
                IsActive = true,
                HasBeenCalled = false
            },
                new Trigger
            {
                Name = "Test Trigger 2",
                TriggerType = Core.Enums.TriggerType.NOTIFICATION,
                NotificationText = "Test Notification 3",
                DeviceId = 2,
                Conditions = ConditionMockData.GetConditionEntities(),
                IsActive = true,
                HasBeenCalled = false
            } ,
            new Trigger
            {
                Name = "Test Trigger 3",
                TriggerType = Core.Enums.TriggerType.BOTH,
                ApiUrl = "http://example.com",
                NotificationText = "Test Notification 3",
                DeviceId = 3,
                Conditions = ConditionMockData.GetConditionEntities(),
                IsActive = true,
                HasBeenCalled = false
            }
        };
        }

        public static Trigger NewTriggerEntity()
        {
            return new Trigger
            {
                Name = "Test Trigger 4",
                TriggerType = Core.Enums.TriggerType.API_CALL,
                ApiUrl = "http://example.com",
                DeviceId = 4,
                Conditions = ConditionMockData.GetConditionEntities(),
                IsActive = true,
                HasBeenCalled = false
            };
        }
    }
}
