namespace LMS.API.Models.DTOs
{
    public class BookAuthorDTO
    {
        public Guid BookAuthorId { get; set; }
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
