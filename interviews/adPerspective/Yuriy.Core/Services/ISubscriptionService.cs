using System.Collections.Generic;
using System.Threading.Tasks;
using Yuriy.Core.Model;

namespace Yuriy.Core.Services
{
    public interface ISubscriptionService
    {
        Task<IEnumerable<Subscription>> GetUserNotificationSubscriptions(int userId);
    }
}