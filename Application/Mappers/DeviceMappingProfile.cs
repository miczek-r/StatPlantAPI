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
                    dest => dest.SensorDataDetails, opt => opt.MapFrom(src => src.SensorData.Where(item => item.DateOfMeasurement >= DateTime.Now.AddDays(-1)).GroupBy(item => new { item.Sensor.SensorType.TypeName, DateOfMeasurement = RoundUp(item.DateOfMeasurement,TimeSpan.FromMinutes(15))}).Select(g=> new SensorDataBase2DTO(){ AverageValue = g.Average(x=>x.Value), DateOfMeasurement = g.Key.DateOfMeasurement, SensorTypeName = g.Key.TypeName}))
                )
                .PreserveReferences();
        }

        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }
    }
}
