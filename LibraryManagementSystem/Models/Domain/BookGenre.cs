namespace LMS.API.Models.Domain
{
    public class BookGenre
    {
        public Guid BookGenreId { get; set; }
        public Guid BookId { get; set; }
        public Guid GenreId { get; set; }

        // Navigation properties
        public Book Book { get; set; }
        public Genre Genre { get; set; }
    }
}
