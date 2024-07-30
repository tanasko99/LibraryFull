using FullLibrary.DTOs;
using FullLibrary.Models;
using FullLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FullLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : BaseController<AuthController>
    {
        private readonly IAuthService _authService;
        public AuthController(
            ILogger<AuthController> logger,
            IAuthService authService
            ) : base(logger)
        {
            _authService = authService;
        }

        /// <summary>
        /// Authorized for: Anybody with valid credentials
        /// </summary>
        /// <response code="200">Authentication is successful, token is retrieved.</response>
        /// <response code="400">Authentication failed</response>
        /// <response code="401">Not authorized</response>
        /// <response code="401">Bad request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginDto model)
        {
            var result = await _authService.Login(model);
            return result switch
            {
                { IsFailed: true } => Results.Unauthorized(),
                { IsSuccess: true } => Results.Ok(result.Value),
                _ => Results.BadRequest("Something went wrong")
            };
        }
    }
}
