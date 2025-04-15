namespace LMS.API.Models.Domain
{
    public class BookDetails
    {
        public Guid BookDetailsId { get; set; }
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid GenreId { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}
