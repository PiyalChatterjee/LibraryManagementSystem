namespace LMS.API.Models.Domain
{
    public class BookAuthor
    {
        public Guid BookAuthorId { get; set; }
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
