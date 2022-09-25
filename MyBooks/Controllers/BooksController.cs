using Microsoft.AspNetCore.Mvc;
using MyBooks.Data.Services;
using MyBooks.Data.ViewModels;

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
        public IActionResult AddBook([FromBody]BookVM book)
        {
            _bookService.AddBook(book); 
            return Ok();
        }

        [HttpGet("get-all")]
        public IActionResult GetAllBooks()
        {
            var all= _bookService.GetAllBooks();
            return Ok(all);
        }

        [HttpGet("get-by-id/{id}")]
        public IActionResult GetAllById(int id)
        {
            var book = _bookService.GetBookById(id);
            return Ok(book);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBookById(int id, [FromBody]BookVM book)
        {
            var updateBook = _bookService.UpdateBookById(id, book);
            return Ok(updateBook);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteById(int id)
        {
            _bookService.DeleteBookById(id);
            return Ok();
        }
    }
}
