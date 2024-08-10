using FullLibrary.DTOs;
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
    }
}
