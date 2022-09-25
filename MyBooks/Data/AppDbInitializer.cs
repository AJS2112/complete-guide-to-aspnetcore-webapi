namespace MyBooks.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                if (!context.Books.Any())
                {
                    context.Books.AddRange(new Models.Book[] {
                    new Models.Book(){
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
                    new Models.Book(){
                        Author = "Second Author",
                        CoverUrl = "https...",
                        DateAdd = DateTime.Now,
                        DateRead = DateTime.Now,
                        Description = "2st Book Description",
                        Genre = "Aventure",
                        IsRead = false,
                        Rate = 4,
                        Title = "2nd Book"
                    }
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
