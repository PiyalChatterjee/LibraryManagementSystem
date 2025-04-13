using LMS.API.Models.Enums;

namespace LMS.API.Models.Domain
{
    public class Member
    {
        public Guid MemberId { get; set; }
        public Guid UserId { get; set; }
        public DateTime MembershipStartDate { get; set; }
        public DateTime MembershipExpiryDate { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public MemberStatus Status { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Navigation property to the Users entity
        public User User { get; set; }
    }
}
