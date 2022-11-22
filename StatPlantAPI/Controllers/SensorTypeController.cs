using Application.DTOs.SensorType;
using Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StatPlantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorTypeController : ControllerBase
    {
        private readonly ISensorTypeService _sensorTypeService;

        public SensorTypeController(ISensorTypeService sensorTypeService)
        {
            _sensorTypeService = sensorTypeService;
        }


        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns all sensor types",
            Description = "Returns all sensor tyeps",
            OperationId = "GetSensorTypes"
            )]
        public async Task<ActionResult<IEnumerable<SensorTypeBaseDTO>>> GetAll()
        {
            return Ok(await _sensorTypeService.GetAll());
        }
    }
}
