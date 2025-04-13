namespace LMS.API.Models.DTOs
{
    public class UpdateUserRequestDTO
    {
        public string Username { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; } // Possible values: Admin, Librarian, Member
        public string Status { get; set; }
    }
}
