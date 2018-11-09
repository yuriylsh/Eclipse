using System.Collections.Generic;
using System.Threading.Tasks;
using Yuriy.Core.Model;

namespace Yuriy.Core.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetUserNotificationSubscriptions(int userId);
        Task<bool> ValidateUpdates(SubscriptionUpdate[] updates);
        Task ApplyUpdates(int userId, SubscriptionUpdate[] updates);
    }
}