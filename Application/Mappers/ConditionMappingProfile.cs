using Application.DTOs.Condition;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class ConditionMappingProfile : Profile
    {
        public ConditionMappingProfile()
        {
            CreateMap<ConditionCreateDTO, Condition>().PreserveReferences();
            CreateMap<ConditionUpdateDTO, Condition>().PreserveReferences();
            CreateMap<Condition, ConditionBaseDTO>().PreserveReferences();
        }
    }
}
