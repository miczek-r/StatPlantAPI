using Application.DTOs.Hub;
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
    public class HubController : ControllerBase
    {
        private readonly IHubService _hubService;

        public HubController(IHubService hubService)
        {
            this._hubService = hubService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns hubs",
            Description = @"Returns all user hubs.",
            OperationId = "GetAllHubs"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The hubs were returned", Type = typeof(IEnumerable<HubLiteDTO>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<IEnumerable<HubLiteDTO>>> GetAll()
        {
            return Ok(await _hubService.GetAll());
        }

        [HttpGet("{hubId}")]
        [SwaggerOperation(
            Summary = "Returns specific hub",
            Description = @"Returns specific hub by provided hub identificator.
                            User must be a part of hub.",
            OperationId = "GetHub"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The hub with provided identificator was returned", Type = typeof(HubBaseDTO))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The hub you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<HubBaseDTO>> Get(int hubId)
        {
            return Ok(await _hubService.GetById(hubId));
        }
    }
}
