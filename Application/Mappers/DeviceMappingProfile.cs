using Application.DTOs.Device;
using Application.DTOs.SensorData;
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
            CreateMap<Device, DeviceLiteDTO>().PreserveReferences();
            CreateMap<Device, DeviceBaseDTO>()
                .ForMember(
                    dest => dest.SensorData, opt => opt.MapFrom(src => src.SensorData.GroupBy(item => item.Sensor.SensorType)
                    .Select(g => g.OrderByDescending(c => c.DateOfMeasurement).FirstOrDefault()).ToList())
                ).ForMember(
                    dest => dest.SensorDataDetails, opt => opt.MapFrom(src => src.SensorData.Where(item => item.DateOfMeasurement >= DateTime.Now.AddDays(-7)).GroupBy(item => new { item.Sensor.SensorType.TypeName, item.DateOfMeasurement.Hour }).Select(g => new SensorDataBase2DTO() { SensorTypeName = g.Key.TypeName, DateOfMeasurement = g.Key.Hour, AverageValue = g.Average(x => x.Value) }))
                )
                .PreserveReferences();
        }
    }
}
