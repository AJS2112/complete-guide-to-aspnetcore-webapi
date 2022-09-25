
using MyBooks.Data.Models;
using MyBooks.Data.ViewModels;
using MyBooks.Exceptions;

namespace MyBooks.Data.Services
{
    public class BookService 
    {
        private AppDbContext _context;
        public BookService(AppDbContext context)
        {
            _context = context;
        }

        public Book AddBook(BookVM book)
        {
            if (!ValidBookTitle(book.Title))
                throw new BookTitleException("invalid name", book.Title);

            var _book = new Book()
            {
                Title = book.Title,
                Description = book.Description,
                IsRead = book.IsRead,
                DateRead = book.IsRead ? book.DateRead : null,
                Rate = book.IsRead ? book.Rate: null,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                Author = book.Author,
                DateAdd = DateTime.Now,
            };

            _context.Books.Add(_book);
            _context.SaveChanges();

            return _book;
        }

        public List<Book> GetAllBooks(string sortBy, string searchString)
        {
            var allBooks = _context.Books.OrderBy(x => x.Title).ToList();

            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "title_desc":
                        allBooks = allBooks.OrderByDescending(x => x.Title).ToList();
                        break;
                    case "author":
                        allBooks = allBooks.OrderBy(x => x.Author).ToList();
                        break;
                    case "author_desc":
                        allBooks = allBooks.OrderByDescending(x => x.Author).ToList();
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                allBooks = allBooks.Where(x => x.Title.Contains(searchString)).ToList();
            }

            return allBooks;
        } 


        public Book GetBookById(int bookId) => _context.Books.FirstOrDefault(x => x.Id == bookId);

        public Book UpdateBookById(int bookId, BookVM book)
        {
            var _book = _context.Books.FirstOrDefault(x => x.Id == bookId);
            if(_book != null)
            {
                _book.Title = book.Title;
                _book.Description = book.Description;
                _book.IsRead = book.IsRead;
                _book.DateRead = book.IsRead ? book.DateRead : null;
                _book.Rate = book.IsRead ? book.Rate : null;
                _book.Genre = book.Genre;
                _book.CoverUrl = book.CoverUrl;
                _book.Author = book.Author;

                _context.SaveChanges();

            }

            return _book;   
        }

        public void DeleteBookById(int id)
        {
            var _book = _context.Books.FirstOrDefault(x => x.Id == id);
            if (_book != null)
            {
                _context.Books.Remove(_book);
                _context.SaveChanges();
            }
        }

        private bool ValidBookTitle(string bookTitle)
        {
            if (bookTitle.Length > 5)
                return false;

            return true;
        }

    }
}
