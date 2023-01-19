using Application.DTOs.Device;
using Application.DTOs.Hub;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.MockData
{
    public class HubMockData
    {
        public static List<HubLiteDTO> GetHubs()
        {
            return new List<HubLiteDTO>
            {
                new HubLiteDTO
                {
                    Id = "testHubId1",
                    Name = "Test Hub 1",
                    Description = "This is Test Hub 1",
                    MacAddress = "testMac1"
                },
                  new HubLiteDTO
                {
                    Id = "testHubId2",
                    Name = "Test Hub 2",
                    Description = "This is Test Hub 2",
                    MacAddress = "testMac2"
                }
            };
        }

        public static HubBaseDTO GetHub()
        {
            return new HubBaseDTO
            {
                Id = "testHubId1",
                Name = "Test Hub 1",
                Description = "This is Test Hub 1",
                MacAddress = "testMac1",
                Users = new List<UserBaseDTO>(),
                Devices = new List<DeviceLiteDTO>()
            };
        }

        public static HubCreateDTO NewHub()
        {
            return new HubCreateDTO
            {
                Name = "Test Hub 1",
                Description = "This is Test Hub 1",
                MacAddress = "testMac1",
                Password = "testPassword1",
                Devices = new List<DeviceCreateDTO>()
            };
        }

        public static HubJoinDTO JoinHubRequest()
        {
            return new HubJoinDTO
            {
                MacAddress = "testMac1",
                Password = "testPassword1"
            };
        }
    }
}
