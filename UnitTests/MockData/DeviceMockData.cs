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
                    Name = "First Test Device",
                    Description = "This is first test device",
                    UUID = "1234567890",
                }
            };
        }
    }
}
