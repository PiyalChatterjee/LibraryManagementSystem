namespace LMS.API.Models.Domain
{
    public class Author
    {
        public Guid AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Biography { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
