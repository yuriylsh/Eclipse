using System.ComponentModel.DataAnnotations.Schema;

namespace Yuriy.Web.Data
{
    public partial class NotificationUnsubscribe
    {
        public int User { get; set; }
        public int Type { get; set; }

        [ForeignKey("Id")]
        [InverseProperty("NotificationUnsubscribe")]
        public NotificationType TypeNavigation { get; set; }
        [ForeignKey("User")]
        [InverseProperty("NotificationUnsubscribe")]
        public User UserNavigation { get; set; }
    }
}
