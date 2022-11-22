using Application.DTOs.SensorData;
using Application.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StatPlantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class SensorDataController : ControllerBase
    {
        private readonly ISensorDataService _sensorDataService;

        public SensorDataController(ISensorDataService sensorDataService)
        {
            this._sensorDataService = sensorDataService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Record data from device sensors",
            Description = @"Records data from sensors.",
            OperationId = "RecordSensorData"
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "The sensors data were registered")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The device sensors data is invalid")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> Post([FromBody] SensorDataCreateDTO sensorDataCreateDTO)
        {
            await _sensorDataService.Create(sensorDataCreateDTO);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<SensorDataDetailsDTO>> Get([FromQuery] SensorDataGetDetailsDTO getDetailsDTO)
        {
            return Ok(await _sensorDataService.GetDetails(getDetailsDTO));
        }
    }
}
