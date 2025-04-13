using LMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.DTOs
{
    public class UpdateUserRequestDTO
    {
        [Required]
        public string Username { get; set; }
        public string? Password { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [EnumDataType(typeof(UserRoles), ErrorMessage = "Invalid role value.")]
        public string Role { get; set; } // Possible values: Admin, Librarian, Member
        [Required]
        [EnumDataType(typeof(UserStatus), ErrorMessage = "Invalid status value.")]
        public string Status { get; set; }
    }
}
