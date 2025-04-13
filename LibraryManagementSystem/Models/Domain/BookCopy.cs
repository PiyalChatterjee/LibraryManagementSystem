using LMS.API.Models.Enums;

namespace LMS.API.Models.Domain
{
    public class BookCopy
    {
        public Guid BookCopyId { get; set; }
        public Guid BookId { get; set; }
        public BookStatus Status { get; set; }
        public string Location { get; set; }
        public DateTime AcquisitionDate { get; set; }

        // Navigation properties
        public Book Book { get; set; }
    }
}
