using Application.DTOs.Device;
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
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;

        public DeviceController(IDeviceService deviceService)
        {
            this._deviceService = deviceService;
        }

        [HttpGet("{deviceId}")]
        [SwaggerOperation(
            Summary = "Returns specific device",
            Description = @"Returns specific device by provided device identificator.
                            User must be a part of hub that device is a part of.",
            OperationId = "GetDevice"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The device with provided identificator was returned", Type = typeof(DeviceBaseDTO))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The device you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<DeviceBaseDTO>> Get(int deviceId)
        {
            return Ok(await _deviceService.GetById(deviceId));
        }
    }
}
