﻿using FullLibrary.DTOs;
using FullLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserController>
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public UserController(ILogger<UserController> logger, IAuthService authService, IUserService userService) : base(logger)
        {
            _authService = authService;
            _userService = userService;
        }

        /// <summary>
        /// Authorized for: Admin role, can register Librarians
        /// </summary>
        /// <param name="model">
        /// Role = Librarian
        /// </param>
        /// <response code="200">Authentication is successful, token is retrieved.</response>
        /// <response code="400">Not authenticated</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPost("register-librarian")]
        public async Task<IResult> RegisterLibrarian([FromBody] RegisterDto model)
        {
            var result = await _authService.RegisterLibrarian(model);
            return result switch
            {
                { IsFailed: true } => Results.BadRequest($"{result.Errors}"),
                { IsSuccess: true } => Results.Ok("Librarian successfuly created"),
                _ => Results.BadRequest("Something went wrong")
            };
        }

        /// <summary>
        /// Authorized for: librarians role, can register user
        /// </summary>
        /// <param name="model">
        /// Role = Librarian
        /// </param>
        /// <response code="200">Authentication is successful, token is retrieved.</response>
        /// <response code="400">Not authenticated</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Librarian", AuthenticationSchemes = "Bearer")]
        [HttpPost("register-user")]
        public async Task<IResult> RegisterUser([FromBody] RegisterDto model)
        {
            var result = await _authService.RegisterUser(model);
            return result switch
            {
                { IsFailed: true } => Results.BadRequest($"{result.Errors}"),
                { IsSuccess: true } => Results.Ok("User successfuly created"),
                _ => Results.BadRequest("Something went wrong")
            };
        }


        /// <summary>
        /// Authorized for: librarian,user
        /// </summary>
        /// <param name="model">
        /// Role = Librarian
        /// </param>
        /// <response code="200">Authentication is successful, token is retrieved.</response>
        /// <response code="400">Not authenticated</response>
        /// <response code="401">Not authorized</response>
        /// <response code="500">Internal server error</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Librarian,User", AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] UpdateProfileDto model)
        {
            try
            {
                _userService.UpdateUser(id, model);
                return Ok("User successfuly updated");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
