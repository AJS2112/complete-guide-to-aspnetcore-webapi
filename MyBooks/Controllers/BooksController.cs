using Microsoft.AspNetCore.Mvc;
using MyBooks.ActionResults;
using MyBooks.Data.Models;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;
using MyBooks.Exceptions;

namespace MyBooks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        public BookService _bookService;

        public BooksController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] BookVM book)
        {
            try
            {
                var _book = _bookService.AddBook(book);
                return Created(nameof(AddBook), _book);
            }
            catch (BookTitleException ex)
            {
                return BadRequest($"{ex.Message}, Book Title: {ex.BookTitle}");

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get-all")]
        public IActionResult GetAllBooks()
        {
            var all = _bookService.GetAllBooks();
            return Ok(all);
        }

        [HttpGet("get-by-id/{id}")]
        public CustomActionResult GetById(int id)
        {
            //throw new Exception("This is an exception that will be handled by middleware");
            var book = _bookService.GetBookById(id);
            if (book != null)
            {
                var _responseObject = new CustomActionResultVM()
                {
                    Data = book
                };

                return new CustomActionResult(_responseObject);
            }
            else
            {
                var _responseObject = new CustomActionResultVM()
                {
                    Exception = new Exception("Exception from Book controller")
                };

                return new CustomActionResult(_responseObject);

                //return NotFound();
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] BookVM book)
        {
            var updateBook = _bookService.UpdateBookById(id, book);
            return Ok(updateBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _bookService.DeleteBookById(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
