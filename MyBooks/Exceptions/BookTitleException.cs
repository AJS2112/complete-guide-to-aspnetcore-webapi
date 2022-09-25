namespace MyBooks.Exceptions
{
    public class BookTitleException : Exception
    {
        public string BookTitle { get; set; }

        public BookTitleException()
        {

        }

        public BookTitleException(string message) : base(message)   
        {

        }

        public BookTitleException(string message, Exception inner):base(message, inner)
        {

        }

        public BookTitleException(string message, string bookTitle): base(message)
        {
            BookTitle = bookTitle;
        }
    }
}
