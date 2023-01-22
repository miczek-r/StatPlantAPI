using Application.DTOs.Device;
using Application.DTOs.Hub;
using Application.DTOs.User;
using Core.Entities;
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

        public static Hub GetHubEntity()
        {
            return new Hub
            {
                Id = 1,
                Name = "Test Hub 1",
                Description = "This is Test Hub 1",
                MacAddress = "testMac1",
                Password = "testPassword1",
                Devices = DeviceMockData.GetDeviceEntities(),
                Users = UserMockData.GetUserEntities()
            };
        }

        public static List<Hub> GetHubEntities()
        {
            return new List<Hub>
            {
            new Hub
            {
                Name = "Test Hub 1",
                Description = "This is Test Hub 1",
                MacAddress = "testMac1",
                Password = "testPassword1",
                Devices = DeviceMockData.GetDeviceEntities(),
                Users = UserMockData.GetUserEntities()
            },
                new Hub
            {
                Name = "Test Hub 2",
                Description = "This is Test Hub 2",
                MacAddress = "testMac2",
                Password = "testPassword2",
                Devices = DeviceMockData.GetDeviceEntities(),
                Users = UserMockData.GetUserEntities()
            },
                new Hub
            {
                Name = "Test Hub 3",
                Description = "This is Test Hub 3",
                MacAddress = "testMac3",
                Password = "testPassword3",
                Devices = DeviceMockData.GetDeviceEntities(),
                Users = UserMockData.GetUserEntities()
            }
            };
        }

        public static Hub NewHubEntity()
        {
            return new Hub
            {
                Name = "Test Hub 4",
                Description = "This is Test Hub 4",
                MacAddress = "testMac4",
                Password = "testPassword4",
                Devices = DeviceMockData.GetDeviceEntities(),
                Users = UserMockData.GetUserEntities()
            };
        }

        public static Hub EmptyHubEntity()
        {
            return new Hub
            {
                Name = "Empty Hub",
                Description = "This is Empty Hub",
                MacAddress = "emptyHub1",
                Password = "emptyHubPassword1",
                Devices = new List<Device>(),
                Users = new List<User>()
            };
        }
    }
}
