using Application.DTOs.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface IEventService
    {
        Task<IEnumerable<EventLiteDTO>> GetEventsForMonth(EventGetByDateDTO getByDateDTO);
        Task<IEnumerable<EventBaseDTO>> GetEventsForDay(EventGetByDateDTO getByDateDTO);
        Task<EventBaseDTO> Get(int eventId);
        Task<int> Create(EventCreateDTO eventCreateDTO);
        Task Update(EventBaseDTO eventUpdateDTO);
        Task Remove(int eventId);
        Task CheckAllEvents();
    }
}
