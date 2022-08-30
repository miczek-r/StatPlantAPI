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
            CreateMap<SensorDataCreateDTO, SensorData>().PreserveReferences();
        }
    }
}
