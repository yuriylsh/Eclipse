﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yuriy.Web.Data
{
    public partial class NotificationWhileUnsubscribed
    {
        public int Id { get; set; }
        public int User { get; set; }
        public int Type { get; set; }
        [Required]
        [StringLength(500)]
        public string Body { get; set; }
        [Column(TypeName = "datetimeoffset(0)")]
        public DateTimeOffset Date { get; set; }

        [ForeignKey("Type")]
        [InverseProperty("NotificationWhileUnsubscribed")]
        public NotificationType TypeNavigation { get; set; }
        [ForeignKey("User")]
        [InverseProperty("NotificationWhileUnsubscribed")]
        public User UserNavigation { get; set; }
    }
}
