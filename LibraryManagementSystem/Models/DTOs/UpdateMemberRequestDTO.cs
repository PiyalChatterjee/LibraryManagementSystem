using LMS.API.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.API.Models.DTOs
{
    public class UpdateMemberRequestDTO
    {
        [Required]
        public DateTime MembershipStartDate { get; set; }
        [Required]
        public DateTime MembershipExpiryDate { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string Phone { get; set; }
        [Required]
        [EnumDataType(typeof(MemberStatus), ErrorMessage = "Invalid status value.")]
        public string Status { get; set; }
    }
}
