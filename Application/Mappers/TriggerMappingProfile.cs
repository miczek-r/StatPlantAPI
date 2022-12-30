using Application.DTOs.Trigger;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class TriggerMappingProfile: Profile
    {
        public TriggerMappingProfile()
        {
            CreateMap<TriggerCreateDTO, Trigger>().PreserveReferences();
            CreateMap<TriggerUpdateDTO, Trigger>().PreserveReferences();
            CreateMap<Trigger,TriggerBaseDTO>().PreserveReferences();
            CreateMap<Trigger,TriggerLiteDTO>().PreserveReferences();
        }
    }
}
