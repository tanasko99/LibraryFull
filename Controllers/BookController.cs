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
    public class BookController : BaseController<BookController>
    {
        private readonly IBookService _bookService;

        public BookController(ILogger<BookController> logger, IBookService bookService) : base(logger)
        {
            _bookService = bookService;
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
        [HttpPost("insert-book")]
        public ActionResult Create([FromBody] BookDto model)
        {
            try
            {
                _bookService.CreateBook(model);
                return Ok("Book is seccessfully created");
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
        public IResult GetBook(int id)
        {
            var result = _bookService.GetBookById(id);
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
        public ActionResult GetAllBooks()
        {
            return Ok(_bookService.GetAllBooks());
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
        public ActionResult UpdateBook(int id, [FromBody] BookDto model)
        {
            try
            {
                _bookService.UpdateBook(id, model);
                return Ok("Book successfuly updated");
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
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Librarian", AuthenticationSchemes = "Bearer")]
        [HttpDelete]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var bookDeleted = _bookService.DeleteBook(id);
            if(!bookDeleted)
            {
                return BadRequest("Book does not exist");
            }
            return Ok("Book is successfully deleted");
        }
    }
}
