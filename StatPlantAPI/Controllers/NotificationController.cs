
using Application.DTOs.Notification;
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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this._notificationService = notificationService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns notifications",
            Description = @"Returns all notifications for user.",
            OperationId = "GetAllNotifications"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The notifications were returned", Type = typeof(IEnumerable<NotificationBaseDTO>))]
        [SwaggerResponse(StatusCodes.Status401Unauthorized, "You must be logged in to access this resource")]
        [SwaggerResponse(StatusCodes.Status403Forbidden, "You do not have permissions to access this resource")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<IEnumerable<NotificationBaseDTO>>> GetAll()
        {
            return Ok(await _notificationService.GetAll());
        }
    }
}
