namespace LMS.API.Models.Domain
{
    public class Genre
    {
        public Guid GenreId { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
