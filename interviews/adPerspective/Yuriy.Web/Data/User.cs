using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yuriy.Core.Model;

namespace Yuriy.Web.Data
{
    public partial class User : IUser
    {
        public User()
        {
            Notification = new HashSet<Notification>();
            NotificationUnsubscribe = new HashSet<NotificationUnsubscribe>();
            NotificationWhileUnsubscribed = new HashSet<NotificationWhileUnsubscribed>();
        }

        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(100)]
        public string LastName { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [InverseProperty("UserNavigation")]
        public ICollection<Notification> Notification { get; set; }
        [InverseProperty("UserNavigation")]
        public ICollection<NotificationUnsubscribe> NotificationUnsubscribe { get; set; }
        [InverseProperty("UserNavigation")]
        public ICollection<NotificationWhileUnsubscribed> NotificationWhileUnsubscribed { get; set; }
    }
}
