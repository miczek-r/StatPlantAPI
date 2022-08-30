using Application.DTOs.SensorType;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class SensorTypeMappingProfile : Profile
    {
        public SensorTypeMappingProfile()
        {
            CreateMap<SensorType, SensorTypeBaseDTO>().PreserveReferences();
            CreateMap<SensorTypeCreateDTO, SensorType>().PreserveReferences();
        }
    }
}
