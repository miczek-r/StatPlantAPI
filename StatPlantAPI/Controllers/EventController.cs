using Application.DTOs.Event;
using Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StatPlantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _service;

        public EventController(IEventService service)
        {
            _service = service;
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<EventBaseDTO>> Get(int eventId)
        {
            return Ok(await _service.Get(eventId));
        }

        [HttpPost("GetAllDayEvents")]
        public async Task<ActionResult<IEnumerable<EventBaseDTO>>> GetByDay([FromBody] EventGetByDateDTO getByDateDTO)
        {
            return Ok(await _service.GetEventsForDay(getByDateDTO));
        }

        [HttpPost("GetAllMonthEvents")]
        public async Task<ActionResult<IEnumerable<EventLiteDTO>>> GetByMonth([FromBody] EventGetByDateDTO getByDateDTO)
        {
            return Ok(await _service.GetEventsForMonth(getByDateDTO));
        }

        [HttpPost]
        public async Task<ActionResult<EventBaseDTO>> Create([FromBody] EventCreateDTO eventCreateDTO)
        {
            int id = await _service.Create(eventCreateDTO);
            EventBaseDTO eventObj = await _service.Get(id);
            return CreatedAtAction(nameof(EventController.Create), new { id }, eventObj);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] EventBaseDTO eventUpdateDTO)
        {
            await _service.Update(eventUpdateDTO);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(int eventId)
        {
            await _service.Remove(eventId);
            return NoContent();
        }

        [HttpPost("CheckAllEvents")]
        public async Task<ActionResult> CheckAllEvents()
        {
            await _service.CheckAllEvents();
            return NoContent();
        }
    }
}
