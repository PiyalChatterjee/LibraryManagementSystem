using LMS.API.Models.Enums;

namespace LMS.API.Models.Domain
{
    public class Notification
    {
        public Guid NotificationId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public NotificatioStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? SentDate { get; set; }


        // Navigation property to the Users entity
        public User User { get; set; }
    }
}
