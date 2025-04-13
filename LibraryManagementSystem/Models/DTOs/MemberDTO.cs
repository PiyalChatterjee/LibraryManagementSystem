namespace LMS.API.Models.DTOs
{
    public class MemberDTO
    {
        public Guid MemberId { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipExpiryDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; } // Possible values: Active, Suspended, Expired
        public DateTime DateOfBirth { get; set; }

        public UserDTO User { get; set; }
    }
}
