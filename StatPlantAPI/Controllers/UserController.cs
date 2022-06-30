using Application.DTOs.User;
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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Returns users",
            Description = @"Returns all users.",
            OperationId = "GetAllUsers"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The users were returned", Type = typeof(IEnumerable<UserBaseDTO>))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<IEnumerable<UserBaseDTO>>> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Returns specific user",
            Description = @"Returns specific user by provided user identificator.",
            OperationId = "GetUser"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "The user with provided identificator was returned", Type = typeof(UserBaseDTO))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The user you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<UserBaseDTO>> Get(string id)
        {
            return Ok(await _userService.GetById(id));
        }


        [HttpPost]
        [SwaggerOperation(
            Summary = "Registers new user",
            Description = @"Registers user using provided data.",
            OperationId = "Register"
            )]
        [SwaggerResponse(StatusCodes.Status201Created, "The user was registered", Type = typeof(UserBaseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The registration data is invalid")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> Register([FromBody] UserCreateDTO registerDTO)
        {
            string id = await _userService.Create(registerDTO);
            UserBaseDTO user = await _userService.GetById(id);
            return CreatedAtAction(nameof(UserController.Register), new { id }, user);
        }
    }
}
