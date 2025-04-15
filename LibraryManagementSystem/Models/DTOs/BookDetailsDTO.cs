namespace LMS.API.Models.DTOs
{
    public class BookDetailsDTO
    {
        public Guid BookDetailsId { get; set; }
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
        public Guid GenreId { get; set; }

        public BookDTO Book { get; set; }
        public AuthorDTO Author { get; set; }
        public GenreDTO Genre { get; set; }

    }
}
