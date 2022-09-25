using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MyBooks.Controllers;
using MyBooks.Data;
using MyBooks.Data.Models;
using MyBooks.Data.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBooksTests
{
    public class BookControllerTest
    {
        private static DbContextOptions<AppDbContext> dbContextOptions = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "BookDbTest")
            .Options;

        AppDbContext context;
        BookService bookService;
        BooksController booksController;

        [OneTimeSetUp]
        public void Setup()
        {
            context = new AppDbContext(dbContextOptions);
            context.Database.EnsureCreated();

            SeedDatabase();

            bookService = new BookService(context);
            booksController = new BooksController(new NullLogger<BooksController>() , bookService);
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
        public void GetAllBooks_WithSortBy_WithSearchString_WithPageNumber_ReturnOk_Test()
        {
            IActionResult actionResult = booksController.GetAllBooks("title_desc", "6st", 1);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>()); 

            var actionResultData= (actionResult as OkObjectResult).Value as List<Book>;

            Assert.That(actionResultData.First().Title, Is.EqualTo("6st Book"));
            Assert.That(actionResultData.First().Id, Is.EqualTo(6));
            Assert.That(actionResultData.Count(), Is.EqualTo(1));

        }
    }
}
