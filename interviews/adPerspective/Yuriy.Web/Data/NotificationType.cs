using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yuriy.Core.Model;

namespace Yuriy.Web.Data
{
    public partial class NotificationType : INotificationType
    {
        public NotificationType()
        {
            Notification = new HashSet<Notification>();
            NotificationUnsubscribe = new HashSet<NotificationUnsubscribe>();
            NotificationWhileUnsubscribed = new HashSet<NotificationWhileUnsubscribed>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty("TypeNavigation")]
        public ICollection<Notification> Notification { get; set; }
        [InverseProperty("TypeNavigation")]
        public ICollection<NotificationUnsubscribe> NotificationUnsubscribe { get; set; }
        [InverseProperty("TypeNavigation")]
        public ICollection<NotificationWhileUnsubscribed> NotificationWhileUnsubscribed { get; set; }
    }
}
