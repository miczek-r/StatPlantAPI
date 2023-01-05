using Application.DTOs.Event;
using AutoMapper;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappers
{
    public class EventMappingProfile : Profile
    {
        public EventMappingProfile()
        {
            CreateMap<EventBaseDTO, Event>().ReverseMap().PreserveReferences();
            CreateMap<EventCreateDTO, Event>().PreserveReferences();
            CreateMap<IEnumerable<Event>, IEnumerable<EventLiteDTO>>().ConvertUsing<EventConverter>();
        }
    }

    class EventConverter : ITypeConverter<IEnumerable<Event>, IEnumerable<EventLiteDTO>>
    {

        public IEnumerable<EventLiteDTO> Convert(IEnumerable<Event> source, IEnumerable<EventLiteDTO> destination, ResolutionContext context)
        {
            IList<EventLiteDTO> result = new List<EventLiteDTO>();
            foreach (Event eventObj in source)
            {
                if(eventObj.EndTime is null)
                {
                    AddEventToResults(result, eventObj.StartTime);
                    continue;
                }
                for (var day = eventObj.StartTime; day.Date <= eventObj.EndTime; day = day.AddDays(1))
                    {
                        AddEventToResults(result, day);

                    }
            }
            return result;
        }

        private static void AddEventToResults(IList<EventLiteDTO> result, DateTime day)
        {
            var resultDay = result.FirstOrDefault(x => x.Date.Date == day.Date);
            if (resultDay is null)
            {
                result.Add(new EventLiteDTO() { Date = day, NumberOfEvents = 1 });
            }
            else
            {
                resultDay.NumberOfEvents++;
            }
        }
    }
}
