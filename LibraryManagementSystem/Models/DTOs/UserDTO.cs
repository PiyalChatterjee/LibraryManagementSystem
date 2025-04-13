namespace LMS.API.Models.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } // Possible values: Admin, Librarian, Member
        public string Status { get; set; } // Possible values: Active, Inactive
        public DateTime DateCreated { get; set; }
        public DateTime? LastLogin { get; set; }
    }
}
