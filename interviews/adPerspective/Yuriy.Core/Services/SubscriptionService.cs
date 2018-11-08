using System.Collections.Generic;
using System.Threading.Tasks;
using Yuriy.Core.Model;
using Yuriy.Core.Repositories;

namespace Yuriy.Core.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
            => _subscriptionRepository = subscriptionRepository;

        public async Task<IEnumerable<Subscription>> GetUserNotificationSubscriptions(int userId)
            => await _subscriptionRepository.GetByUser(userId);
    }
}