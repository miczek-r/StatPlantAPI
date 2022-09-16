using Application.DTOs.Device;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class DeviceMappingProfile : Profile
    {
        public DeviceMappingProfile()
        {
            CreateMap<DeviceCreateDTO, Device>().PreserveReferences();
            CreateMap<Device, DeviceBaseDTO>().PreserveReferences();
        }
    }
}
