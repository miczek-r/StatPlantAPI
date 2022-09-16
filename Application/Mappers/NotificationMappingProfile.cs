using Application.DTOs.Notification;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class NotificationMappingProfile : Profile
    {
        public NotificationMappingProfile()
        {
            CreateMap<NotificationCreateDTO, Notification>().PreserveReferences();
            CreateMap<Notification, NotificationBaseDTO>().PreserveReferences();
        }
    }
}
