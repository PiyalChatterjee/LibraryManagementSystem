using LMS.API.Models.Enums;

namespace LMS.API.Models.Domain
{
    public class Fine
    {
        public Guid FineId { get; set; }
        public Guid BookBorrowingId { get; set; }
        public decimal Amount { get; set; }
        public DateTime IssuedDate { get; set; }
        public FineStatus Status { get; set; }
        public DateTime? PaidDate { get; set; }
        public string? PaymentMethod { get; set; }

        // Navigation properties
        public BookBorrowing BookBorrowing { get; set; }
    }
}
