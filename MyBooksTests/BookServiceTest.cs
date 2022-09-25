using Microsoft.EntityFrameworkCore;
using MyBooks.Data;
using MyBooks.Data.Models;
using MyBooks.Data.Services;
using NUnit.Framework;
using System;
using System.Linq;

namespace MyBooksTests
{
    public class BookServiceTests
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;

        AppDbContext context;

        BookService bookService;    

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            bookService = new BookService(context); 
        }

        private void SeedDatabase()
        {
            context.Books.AddRange(new Book[] {
                    new Book(){
                        Author = "First Author",
                        CoverUrl = "https...",
                        DateAdd = DateTime.Now,
                        DateRead = DateTime.Now,
                        Description = "1st Book Description",
                        Genre = "Biography",
                        IsRead = true,
                        Rate = 4,
                        Title = "1st Book"
                    },
                    new Book(){
                        Author = "Second Author",
                        CoverUrl = "https...",
                        DateAdd = DateTime.Now,
                        DateRead = DateTime.Now,
                        Description = "2st Book Description",
                        Genre = "Aventure",
                        IsRead = false,
                        Rate = 4,
                        Title = "2nd Book"
                    },
                    new Book(){
                        Author = "Third Author",
                        CoverUrl = "https...",
                        DateAdd = DateTime.Now,
                        DateRead = DateTime.Now,
                        Description = "3rd Book Description",
                        Genre = "Biography",
                        IsRead = true,
                        Rate = 4,
                        Title = "3rd Book"
                    },
                    new Book(){
                        Author = "Fourth Author",
                        CoverUrl = "https...",
                        DateAdd = DateTime.Now,
                        DateRead = DateTime.Now,
                        Description = "4th Book Description",
                        Genre = "Aventure",
                        IsRead = false,
                        Rate = 4,
                        Title = "4th Book"
                    },
                    new Book(){
                        Author = "Fifth Author",
                        CoverUrl = "https...",
                        DateAdd = DateTime.Now,
                        DateRead = DateTime.Now,
                        Description = "5th Book Description",
                        Genre = "Biography",
                        IsRead = true,
                        Rate = 4,
                        Title = "5th Book"
                    },
                    new Book(){
                        Author = "Sixth Author",
                        CoverUrl = "https...",
                        DateAdd = DateTime.Now,
                        DateRead = DateTime.Now,
                        Description = "6st Book Description",
                        Genre = "Aventure",
                        IsRead = false,
                        Rate = 4,
                        Title = "6st Book"
                    }
                    });

            context.SaveChanges();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            context.Database.EnsureDeleted();
        }


        [Test]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = bookService.GetAllBooks("", "", null);

            Assert.That(result.Count,Is.EqualTo(5));
        }

        [Test]
        public void GetAllPublishers_WithNoSortBy_WithNoSearchString_WithPageNumber_Test()
        {
            var result = bookService.GetAllBooks("", "", 2);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllPublishers_WithNoSortBy_WithSearchString_WithNoPageNumber_Test()
        {
            var result = bookService.GetAllBooks("", "3", null);

            Assert.That(result.Count, Is.EqualTo(1));
        }

        [Test]
        public void GetAllPublishers_WithSortBy_WithNoSearchString_WithNoPageNumber_Test()
        {
            var result = bookService.GetAllBooks("title_desc", "", null);

            Assert.That(result.Count, Is.EqualTo(5));
            Assert.That(result.FirstOrDefault().Title, Is.EqualTo("6st Book"));
        }

        [Test]
        public void GetBookById_WithResponse_Test()
        {
            var result = bookService.GetBookById(1);

            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Title, Is.EqualTo("1st Book"));
        }

        [Test]
        public void GetBookById_WithNoResponse_Test()
        {
            var result = bookService.GetBookById(99);

            Assert.That(result, Is.Null);
        }
    }
}