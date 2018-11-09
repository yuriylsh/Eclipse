using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yuriy.Core.Model;
using Yuriy.Core.Repositories;

namespace Yuriy.Core.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private delegate Task DoUpdate(int userId, IEnumerable<int> notificationTypes);

        public SubscriptionService(ISubscriptionRepository subscriptionRepository)
            => _subscriptionRepository = subscriptionRepository;

        public async Task<IEnumerable<Subscription>> GetUserNotificationSubscriptions(int userId)
            => await _subscriptionRepository.GetByUser(userId);

        public async Task<bool> ValidateUpdates(SubscriptionUpdate[] updates)
        {
            var ids = updates.Select(x => x.Id).ToArray();
            var hasDuplicateIds = ids.Distinct().Count() != updates.Length;
            return !hasDuplicateIds && await _subscriptionRepository.AllNotificationTypesExist(ids);
        }

        public async Task ApplyUpdates(int userId, SubscriptionUpdate[] updates)
        {
            await ApplyUpdates(x => !x.IsUnsubscribed, _subscriptionRepository.SubscribeToNotifications);
            await ApplyUpdates(x => x.IsUnsubscribed, _subscriptionRepository.UnsubscribeFromNotifications);

            async Task ApplyUpdates(
                Func<SubscriptionUpdate, bool> predicate,
                DoUpdate update)
            {
                var toApply = updates.Where(predicate).Select(x => x.Id).ToArray();
                if (toApply.Length > 0)
                {
                    await update(userId, toApply);
                }
            }
        }
    }
}