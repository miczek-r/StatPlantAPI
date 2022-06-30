using Application.DTOs.Authentication;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StatPlantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Consumes("application/json")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("/login")]
        [SwaggerOperation(
            Summary = "Logs user in",
            Description = @"Logs user in using provided data.
                            Returns JWT token with its expiration date",
            OperationId = "Login"
            )]
        [SwaggerResponse(StatusCodes.Status200OK, "Login success", Type = typeof(LoginResponseDTO))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "User data is invalid, he is not activated or he is locked in")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult<LoginResponseDTO>> Login([FromBody] LoginDTO loginDTO)
        {
            return Ok(await _authenticationService.Login(loginDTO));
        }

        //TODO: Think if PUT is not better
        [HttpPost("ConfirmMail")]
        [SwaggerOperation(
            Summary = "Confirms account",
            Description = @"Confirms account using provided data.",
            OperationId = "ConfirmMail"
            )]
        [SwaggerResponse(StatusCodes.Status204NoContent, "The account was confirmed")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The confirmation data is invalid")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "The user you are looking for does not exists")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "Oops! Internal Server Error. Try again later")]
        public async Task<ActionResult> ConfirmMail([FromBody] EmailConfirmationDTO confirmationDTO)
        {
            await _authenticationService.ConfirmEmail(confirmationDTO);
            return NoContent();
        }

    }
}
