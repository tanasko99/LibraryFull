using FullLibrary.DTOs;
using FullLibrary.Models;
using FullLibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : BaseController<AuthorController>
    {
        private readonly IAuthorService _authorService;

        public AuthorController(
            ILogger<AuthorController> logger,
            IAuthorService authorService) : base(logger)
        {
            _authorService = authorService;
        }

        /// <summary>
        /// Authorized for: Admin | Librarian
        /// </summary>
        /// <response code="200">Request successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authenticated</response>
        /// <response code="403">Not authorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Admin,Librarian", AuthenticationSchemes = "Bearer")]
        [HttpPost("insert-author")]
        public ActionResult CreateAuthor([FromBody] AuthorDto model)
        {
            try
            {
                _authorService.CreateAuthor(model);
                return Ok("Author is successfully created");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Authorized for: Librarian
        /// </summary>
        /// <response code="200">Request successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authenticated</response>
        /// <response code="403">Not authorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Librarian", AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        public ActionResult UpdateAuthor(int id, [FromBody] AuthorUpdateDto model)
        {
            try
            {
                _authorService.UpdateAuthor(id, model);
                return Ok("Author successfuly updated");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        /// <summary>
        /// Authorized for: Admin | Librarian
        /// </summary>
        /// <response code="200">Request successful</response>
        /// <response code="401">Not authenticated</response>
        /// <response code="403">Not authorized</response>
        /// <response code="500">Bad request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Admin,Librarian", AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public IResult GetAuthor(int id)
        {
            var result = _authorService.GetAuthorById(id);
            return result switch
            {
                { IsFailed: true } => Results.BadRequest($"{result.Errors}"),
                { IsSuccess: true } => Results.Ok(result.Value),
                _ => Results.BadRequest("Something went wrong")
            };
        }
        /// <summary>
        /// Authorized for: Admin | Librarian
        /// </summary>
        /// <response code="200">Request successful</response>
        /// <response code="401">Not authenticated</response>
        /// <response code="403">Not authorized</response>
        /// <response code="500">Bad request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Admin,Librarian", AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public ActionResult GetAllAuthors()
        {
            return Ok(_authorService.GetAllAuthors());
        }

        /// <summary>
        /// Authorized for: Librarian
        /// </summary>
        /// <response code="200">Request successful</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Not authenticated</response>
        /// <response code="403">Not authorized</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[Authorize(Roles = "Librarian", AuthenticationSchemes = "Bearer")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<Author>> DeleteAuthor(int id)
        {
            var authorDeleted = _authorService.DeleteAuthor(id);
            if (!authorDeleted)
            {
                return BadRequest("Author does not exist!");
            }
            return Ok("Author is successfully deleted!");
        }
    }
}
