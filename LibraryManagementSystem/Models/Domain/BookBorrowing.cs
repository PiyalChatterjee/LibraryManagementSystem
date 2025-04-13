using LMS.API.Models.Enums;

namespace LMS.API.Models.Domain
{
    public class BookBorrowing
    {
        public Guid BookBorrowingId { get; set; }
        public Guid BookCopyId { get; set; }
        public Guid MemberId { get; set; }
        public DateTime BorrowedDate { get; set; }
        public DateTime DueDate { get; set; }
        public BookBorrowingStatus Status { get; set; }
        public Guid CheckedOutByUserId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public Guid? LibrarianUserId { get; set; }


        // Navigation properties
        public BookCopy BookCopy { get; set; }
        public Member Member { get; set; }
        public User CheckedOutByUser { get; set; }
        public User? Librarian { get; set; }
    }
}
