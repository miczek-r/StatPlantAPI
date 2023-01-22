using Application.DTOs.Device;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class DeviceMockData
    {
        public static List<DeviceBaseDTO> GetDevices()
        {
            return new List<DeviceBaseDTO>
            {
                new DeviceBaseDTO
                {
                    Name = "Test Device 1",
                    Description = "This is first test device",
                    UUID = "1234567890",
                }
            };
        }

        public static Device GetDeviceEntity()
        {
            return new Device
            {
                Name = "Test Device 1",
                Description = "This is Test Device 1",
                UUID = "1234567890",
                Triggers = TriggerMockData.GetTriggerEntities(),
                SensorData = SensorDataMockData.GetSensorDataEntities()
            };
        }

        public static List<Device> GetDeviceEntities()
        {
            return new List<Device>
            {
            new Device
            {
                Name = "Test Device 1",
                Description = "This is Test Device 1",
                UUID = "1234567890",
                Triggers = TriggerMockData.GetTriggerEntities(),
                SensorData = SensorDataMockData.GetSensorDataEntities(),
                Hub = HubMockData.EmptyHubEntity()
            },
                new Device
            {
                Name = "Test Device 2",
                Description = "This is Test Device 2",
                UUID = "2234567890",
                Triggers = TriggerMockData.GetTriggerEntities(),
                SensorData = SensorDataMockData.GetSensorDataEntities(),
                Hub = HubMockData.EmptyHubEntity()
            },
                new Device
            {
                Name = "Test Device 3",
                Description = "This is Test Device 3",
                UUID = "1234567890",
                Triggers = TriggerMockData.GetTriggerEntities(),
                SensorData = SensorDataMockData.GetSensorDataEntities(),
                Hub = HubMockData.EmptyHubEntity()
            }
            };
        }

        public static Device NewDeviceEntity()
        {
            return new Device
            {
                Name = "Test Device 4",
                Description = "This is Test Device 4",
                UUID = "4234567890",
                Triggers = TriggerMockData.GetTriggerEntities(),
                SensorData = SensorDataMockData.GetSensorDataEntities(),
                Hub = HubMockData.EmptyHubEntity()
            };
        }
    }
}
