using System.Collections.Generic;
using System.Threading.Tasks;
using Yuriy.Core.Model;

namespace Yuriy.Core.Repositories
{
    public interface ISubscriptionRepository
    {
        Task<IEnumerable<Subscription>> GetByUser(int id);
        Task<bool> AllNotificationTypesExist(int[] ids);

        Task UnsubscribeFromNotifications(int userId, IEnumerable<int> notificationTypes);

        Task SubscribeToNotifications(int userId, IEnumerable<int> notificationTypes);
    }
}