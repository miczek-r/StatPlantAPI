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
    public class SensorDataMappingProfile : Profile
    {
        public SensorDataMappingProfile()
        {
            CreateMap<SensorData, SensorDataBaseDTO>().PreserveReferences();
            CreateMap<SensorData, SensorDataLiteDTO>()
                .ForMember(dest => dest.SensorTypeName, opt => opt.MapFrom(src => src.Sensor.SensorType.TypeName)).ForMember(dest => dest.SensorTypeId, opt => opt.MapFrom(src=> src.Sensor.SensorType.Id))
                .PreserveReferences();
            CreateMap<SensorDataCreateDTO, SensorData>().PreserveReferences();
            CreateMap<SensorData, SensorDataRecordDTO>().ForMember(dest => dest.MeasuredValue, opt => opt.MapFrom(src => src.Value)).PreserveReferences();
        }
    }
}
