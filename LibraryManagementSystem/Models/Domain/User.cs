using LMS.API.Models.Enums;

namespace LMS.API.Models.Domain
{
    public class User
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserRoles Role { get; set; }
        public DateTime DateCreated { get; set; }
        public UserStatus Status { get; set; }
        public DateTime? LastLogin { get; set; }

        // Add this property to fix the error
        public ICollection<BookBorrowing> BookBorrowings { get; set; }
    }
}
