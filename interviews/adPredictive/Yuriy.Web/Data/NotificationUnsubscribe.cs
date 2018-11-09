using System.ComponentModel.DataAnnotations.Schema;
using Yuriy.Core.Model;

namespace Yuriy.Web.Data
{
    public partial class NotificationUnsubscribe : INotificationUnsubscribe
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
