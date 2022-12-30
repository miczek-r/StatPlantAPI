using Application.DTOs.Trigger;
using Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StatPlantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TriggerController : ControllerBase
    {
        private readonly ITriggerService _service;

        public TriggerController(ITriggerService service)
        {
            _service = service;
        }

        [HttpGet("ByDeviceId/{deviceId}")]
        public async Task<ActionResult<IEnumerable<TriggerLiteDTO>>> GetByDeviceId(int deviceId)
        {
            return Ok(await _service.GetAll(deviceId));
        }

        [HttpGet("{triggerId}")]
        public async Task<ActionResult<TriggerBaseDTO>> GetById(int triggerId)
        {
            return Ok(await _service.Get(triggerId));
        }

        [HttpPost]
        public async Task<ActionResult<TriggerBaseDTO>> Create([FromBody] TriggerCreateDTO triggerCreateDTO)
        {
            int id = await _service.Create(triggerCreateDTO);
            TriggerBaseDTO trigger = await _service.Get(id);
            return CreatedAtAction(nameof(TriggerController.Create), new { id }, trigger);
        }
    }
}
