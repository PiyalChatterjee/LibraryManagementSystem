namespace LMS.API.Models.DTOs
{
    public class UpdateBookRequestDTO
    {
        public string Title { get; set; }
        public int PublicationYear { get; set; }
        public int TotalCopies { get; set; }
        public int AvailableCopies { get; set; }
        public string Status { get; set; } // Possible values: Available, Borrowed, Reserved, Lost
        public DateTime AddedDate { get; set; }
        public string? Publisher { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? Summary { get; set; }
        public string? Language { get; set; }
        public int? Pages { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
