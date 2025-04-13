using LMS.API.Models.Enums;

namespace LMS.API.Models.Domain
{
    public class Book
    {
        public Guid BookId { get; set; }
        public string ISBN { get; set; }
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public BookStatus Status { get; set; }
        public DateTime AddedTime { get; set; }
        public string? Publisher { get; set; }
        public string? CoverImageURL { get; set; }
        public string? Description { get; set; }
        public string? Language { get; set; }
        public int? Pages { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
