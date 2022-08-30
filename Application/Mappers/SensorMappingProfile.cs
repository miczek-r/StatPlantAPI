using Application.DTOs.Sensor;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class SensorMappingProfile : Profile
    {
        public SensorMappingProfile()
        {
            CreateMap<Sensor, SensorBaseDTO>().PreserveReferences();
            CreateMap<SensorCreateDTO,Sensor>().PreserveReferences();
        }
    }
}
