using System;

namespace Yuriy.Core.Model
{
    public interface INotificationWhileUnsubscribed
    {
        int Id { get; set; }
        int User { get; set; }
        int Type { get; set; }
        string Body { get; set; }
        DateTimeOffset Date { get; set; }
    }
}