using Application.DTOs.Hub;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class HubMappingProfile : Profile
    {
        public HubMappingProfile()
        {
            CreateMap<HubCreateDTO, Hub>().PreserveReferences();
            CreateMap<Hub, HubBaseDTO>().PreserveReferences();
            CreateMap<Hub, HubLiteDTO>().PreserveReferences();
        }
    }
}
